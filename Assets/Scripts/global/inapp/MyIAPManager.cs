using System;
using System.Collections.Generic;
using advert.@event;
using TMPro;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using Product = UnityEngine.Purchasing.Product;

namespace global.inapp
{
    public class MyIAPManager : MonoBehaviour, IStoreListener, IGameManager
    {
        private static IStoreController m_StoreController; // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
        private static UnityEngine.Purchasing.Product test_product = null;

        IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

        // public static string GOLD_50 = "gold50";
        // public static string MYSUB = "mysub";
        public static string TEST_SUB = "test_sub";

        // public static string NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION_2 = "noads";
        public static string TEST = "test";

        public TMP_Text myText;

        private Boolean return_complete = true;

        public ManagerStatus Status { get; private set; }

        public void Startup()
        {
            Debug.Log("MyIAPManager starting...");
            Status = ManagerStatus.Initializing;


            Status = ManagerStatus.Started;
            Debug.Log("MyIAPManager: started");
        }

        async void Start()
        {
            MyDebug("MyIAPManager starting...");

            try
            {
                await UnityServices.InitializeAsync();
                List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();

                // If we haven't set up the Unity Purchasing reference
                if (m_StoreController == null)
                {
                    // Begin to configure our connection to Purchasing
                    InitializePurchasing();
                }
            }
            catch (ConsentCheckException e)
            {
                MyDebug("Consent: :" +
                        e.ToString()); // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
            }

            MyAction += myFunction;

            MyDebug("MyIAPManager: started");
        }


        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.Configure<IGooglePlayConfiguration>().SetDeferredPurchaseListener(OnDeferredPurchase);
            builder.Configure<IGooglePlayConfiguration>().SetQueryProductDetailsFailedListener(MyAction);

            // builder.AddProduct(GOLD_50, ProductType.Consumable);
            // builder.AddProduct(NO_ADS, ProductType.NonConsumable);
            // builder.AddProduct(NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION, ProductType.Subscription);

            // builder.AddProduct(NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION, ProductType.Subscription, new IDs
            // {
            //     {NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION, GooglePlay.Name},
            //     // {NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION, MacAppStore.Name}
            // });         
            builder.AddProduct(TEST_SUB, ProductType.Subscription, new IDs
            {
                { TEST_SUB, GooglePlay.Name },
                // {NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION, MacAppStore.Name}
            });
            // builder.AddProduct(NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION_3, ProductType.Subscription, new IDs
            // {
            //     {NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION_3, GooglePlay.Name},
            //     // {NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION, MacAppStore.Name}
            // }); 
            builder.AddProduct(TEST, ProductType.Consumable, new IDs
            {
                { TEST, GooglePlay.Name },
                // {TEST, MacAppStore.Name}
            });
            UnityPurchasing.Initialize(this, builder);
        }

        private event Action<int> MyAction;

        void myFunction(int myInt)
        {
            MyDebug("Listener = " + myInt.ToString());
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        void OnDeferredPurchase(UnityEngine.Purchasing.Product product)
        {
            MyDebug($"Purchase of {product.definition.id} is deferred");
        }

        public void BuySubscription()
        {
            // BuyProductID(NO_ADS_BASE_ONE_WEEK_SUBSCRIPTION);
        }

        public void BuyGold50()
        {
            // BuyProductID(GOLD_50);
        }

        public void BuyNoAds()
        {
            BuyProductID(TEST_SUB);
        }

        public void CompletePurchase()
        {
            if (test_product == null)
                MyDebug("Cannot complete purchase, product not initialized.");
            else
            {
                m_StoreController.ConfirmPendingPurchase(test_product);
                MyDebug("Completed purchase with " + test_product.transactionID.ToString());
            }
        }

        public void ToggleComplete()
        {
            return_complete = !return_complete;
            MyDebug("Complete = " + return_complete.ToString());
        }

        public void RestorePurchases()
        {
            m_StoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
            {
                if (result)
                {
                    MyDebug("Restore purchases succeeded.");
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "restore_success", true },
                    };
                    AnalyticsService.Instance.CustomData("myRestore", parameters);
                }
                else
                {
                    MyDebug("Restore purchases failed.");
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        { "restore_success", false },
                    };
                    AnalyticsService.Instance.CustomData("myRestore", parameters);
                }

                AnalyticsService.Instance.Flush();
            });
        }

        void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                UnityEngine.Purchasing.Product product = m_StoreController.products.WithID(productId);

                MyDebug($"product: {product.definition}");
                if (product != null && product.availableToPurchase)
                {
                    MyDebug(string.Format("Purchasing product:" + product.definition.id.ToString()));
                    m_StoreController.InitiatePurchase(product, GooglePlay.Name); // todo remove store name
                }
                else
                {
                    MyDebug(
                        "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                MyDebug("BuyProductID FAIL. Not initialized.");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            MyDebug("OnInitialized: PASS");

            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();

            checkSubscription();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            MyDebug("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            MyDebug("OnInitializeFailed InitializationFailureReason:" + error + " message: " + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            test_product = args.purchasedProduct;

            // var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
            //var result = validator.Validate(args.purchasedProduct.receipt);
            //MyDebug("Validate = " + result.ToString());

            if (m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(test_product))
            {
                //The purchase is Deferred.
                //Therefore, we do not unlock the content or complete the transaction.
                //ProcessPurchase will be called again once the purchase is Purchased.
                return PurchaseProcessingResult.Pending;
            }

            if (return_complete)
            {
                MyDebug(string.Format("ProcessPurchase: Complete. Product:" + args.purchasedProduct.definition.id +
                                      " - " +
                                      test_product.transactionID.ToString()));
                Messenger<string>.Broadcast(IapEvent.BUY_SUCCESSFUL, test_product.definition.id);
                return PurchaseProcessingResult.Complete;
            }
            else
            {
                MyDebug(string.Format("ProcessPurchase: Pending. Product:" + args.purchasedProduct.definition.id +
                                      " - " +
                                      test_product.transactionID.ToString()));
                return PurchaseProcessingResult.Pending;
            }
        }

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            MyDebug(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",
                product.definition.storeSpecificId, failureReason));
        }

        public void ListPurchases()
        {
            MyDebug("ListPurchases  " + m_StoreController.products.ToString());
            foreach (UnityEngine.Purchasing.Product item in m_StoreController.products.all)
            {
                if (item.hasReceipt)
                {
                    MyDebug("In list for  " + item.receipt.ToString());
                }
                else
                    MyDebug("No receipt for " + item.definition.id.ToString());
            }
        }

        private void MyDebug(string debug)
        {
            Debug.Log(debug);
            if (myText != null)
            {
                myText.text += "\r\n" + debug;
            }
        }

        public bool hasGoogleSubscription(string subscriptionName)
        {
            if (m_StoreController != null)
            {
                foreach (Product item in m_StoreController.products.all)
                {
                    if (item.availableToPurchase && item.definition.id.Equals(subscriptionName))
                    {
                        if (item.receipt != null && item.definition.type == ProductType.Subscription)
                        {
                            // if (checkIfProductIsAvailableForSubscriptionManager(item.receipt))
                            // {
                            // GooglePurchaseState googlePurchaseState =
                                // m_GooglePlayStoreExtensions.GetPurchaseState(item);
                            // return googlePurchaseState == GooglePurchaseState.Purchased;
                            return item.hasReceipt;
                            // }
                        }
                    }
                }
            }

            return false;
        }

        private bool checkSubscription()
        {
            Dictionary<string, string> introductory_info_dict = m_StoreExtensionProvider
                .GetExtension<IAppleExtensions>().GetIntroductoryPriceDictionary();
            Debug.Log("Available items:");
            foreach (var item in m_StoreController.products.all)
            {
                if (item.availableToPurchase)
                {
                    MyDebug(string.Join(" - ",
                        new[]
                        {
                            item.metadata.localizedTitle,
                            item.metadata.localizedDescription,
                            item.metadata.isoCurrencyCode,
                            item.metadata.localizedPrice.ToString(),
                            item.metadata.localizedPriceString,
                            item.transactionID,
                            item.receipt
                        }));

                    if (item.metadata.localizedDescription.Equals(TEST_SUB))
                    {
                        MyDebug("Wiiihaaa we found it");
                    }

                    // this is the usage of SubscriptionManager class
                    if (item.receipt != null)
                    {
                        if (item.definition.type == ProductType.Subscription)
                        {
                            if (checkIfProductIsAvailableForSubscriptionManager(item.receipt))
                            {
                                string intro_json =
                                    (introductory_info_dict == null ||
                                     !introductory_info_dict.ContainsKey(item.definition.storeSpecificId))
                                        ? null
                                        : introductory_info_dict[item.definition.storeSpecificId];
                                SubscriptionManager p = new SubscriptionManager(item, intro_json);
                                SubscriptionInfo info = p.getSubscriptionInfo();
                                MyDebug("product id is: " + info.getProductId());
                                MyDebug("purchase date is: " + info.getPurchaseDate());
                                MyDebug("subscription next billing date is: " + info.getExpireDate());
                                MyDebug("is subscribed? " + info.isSubscribed().ToString());
                                MyDebug("is expired? " + info.isExpired().ToString());
                                MyDebug("is cancelled? " + info.isCancelled());
                                MyDebug("product is in free trial peroid? " + info.isFreeTrial());
                                MyDebug("product is auto renewing? " + info.isAutoRenewing());
                                MyDebug("subscription remaining valid time until next billing date is: " +
                                        info.getRemainingTime());
                                MyDebug("is this product in introductory price period? " +
                                        info.isIntroductoryPricePeriod());
                                MyDebug("the product introductory localized price is: " + info.getIntroductoryPrice());
                                MyDebug(
                                    "the product introductory price period is: " + info.getIntroductoryPricePeriod());
                                MyDebug("the number of product introductory price period cycles is: " +
                                        info.getIntroductoryPricePeriodCycles());
                            }
                            else
                            {
                                MyDebug(
                                    "This product is not available for SubscriptionManager class, only products that are purchase by 1.19+ SDK can use this class.");
                            }
                        }
                        else
                        {
                            MyDebug("the product is not a subscription product");
                        }
                    }
                    else
                    {
                        MyDebug("the product should have a valid receipt");
                    }
                }
            }

            return false;
        }

        private bool checkIfProductIsAvailableForSubscriptionManager(string receipt)
        {
            var receipt_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
            if (!receipt_wrapper.ContainsKey("Store") || !receipt_wrapper.ContainsKey("Payload"))
            {
                Debug.Log("The product receipt does not contain enough information");
                return false;
            }

            var store = (string)receipt_wrapper["Store"];
            var payload = (string)receipt_wrapper["Payload"];

            if (payload != null)
            {
                switch (store)
                {
                    case GooglePlay.Name:
                    {
                        var payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
                        if (!payload_wrapper.ContainsKey("json"))
                        {
                            Debug.Log(
                                "The product receipt does not contain enough information, the 'json' field is missing");
                            return false;
                        }

                        var original_json_payload_wrapper =
                            (Dictionary<string, object>)MiniJson.JsonDecode((string)payload_wrapper["json"]);
                        if (original_json_payload_wrapper == null ||
                            !original_json_payload_wrapper.ContainsKey("developerPayload"))
                        {
                            Debug.Log(
                                "The product receipt does not contain enough information, the 'developerPayload' field is missing");
                            return false;
                        }

                        var developerPayloadJSON = (string)original_json_payload_wrapper["developerPayload"];
                        var developerPayload_wrapper =
                            (Dictionary<string, object>)MiniJson.JsonDecode(developerPayloadJSON);
                        if (developerPayload_wrapper == null ||
                            !developerPayload_wrapper.ContainsKey("is_free_trial") ||
                            !developerPayload_wrapper.ContainsKey("has_introductory_price_trial"))
                        {
                            Debug.Log(
                                "The product receipt does not contain enough information, the product is not purchased using 1.19 or later");
                            return false;
                        }

                        return true;
                    }
                    case AppleAppStore.Name:
                    case AmazonApps.Name:
                    case MacAppStore.Name:
                    {
                        return true;
                    }
                    default:
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}