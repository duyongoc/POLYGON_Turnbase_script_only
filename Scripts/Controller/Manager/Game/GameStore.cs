// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using Unity.Services.Core;
// using Unity.Services.Core.Environments;
// using UnityEngine;
// using UnityEngine.Purchasing;
// using UnityEngine.Purchasing.Extension;

// public class GameStore : MonoBehaviour, IDetailedStoreListener
// {


//     [Header("[Setting]")]
//     [SerializeField] private bool useFakeStore = false;
//     // [SerializeField] private ProductCatalog catalog;


//     // [private]
//     private Action cbPurchaseSuccess;
//     private List<Product> products = new();
//     private IStoreController storeController;
//     private IExtensionProvider extensionProvider;


//     // [properties]
//     public List<Product> Products { get => products; set => products = value; }
//     public IStoreController StoreController { get => storeController; set => storeController = value; }




//     #region UNITY
//     // private void Start()
//     // {
//     // }

//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             BuyCoin();
//         }
//     }
//     #endregion



//     public void Init()
//     {
//         InitIAP();
//     }


//     private async void InitIAP()
//     {
//         var options = new InitializationOptions()
// #if UNITY_EDITOR
//             .SetEnvironmentName("test");
// #else
//             .SetEnvironmentName("production");
// #endif
//         await UnityServices.InitializeAsync();
//         SetupBuilder();
//     }


//     private void SetupBuilder()
//     {
//         // ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
//         // Debug.Log($"Loaded catalog with {catalog.allProducts.Count} items");
// #if UNITY_EDITOR
//         if (useFakeStore)
//         {
//             // Comment out this line if you are building the game for publishing.
//             StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
//             StandardPurchasingModule.Instance().useFakeStoreAlways = true;
//         }
// #endif

// #if UNITY_ANDROID
//         ConfigurationBuilder builder = ConfigurationBuilder.Instance(
//             StandardPurchasingModule.Instance(AppStore.GooglePlay)
//         );
// #elif UNITY_IOS
//         ConfigurationBuilder builder = ConfigurationBuilder.Instance(
//             StandardPurchasingModule.Instance(AppStore.AppleAppStore)
//         );
// #else
//         ConfigurationBuilder builder = ConfigurationBuilder.Instance(
//             StandardPurchasingModule.Instance(AppStore.NotSpecified)
//         );
// #endif

//         var itemStore = LocalManager.Instance.Game.gameItemStore;
//         foreach (var item in itemStore)
//         {
//             builder.AddProduct(item.key, ProductType.Consumable);
//         }

//         UnityPurchasing.Initialize(this, builder);
//     }


//     public void InitiatePurchase(Product Product, Action callback)
//     {
//         cbPurchaseSuccess = callback;
//         storeController.InitiatePurchase(Product);
//     }


//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         storeController = controller;
//         extensionProvider = extensions;
//         Debug.Log($"Successfully Initialized Unity IAP. Store Controller has {storeController.products.all.Length} products");
//         // foreach (var product in storeController.products.all)
//         // {
//         //     Debug.Log(product.metadata.localizedTitle);
//         //     Debug.Log(product.metadata.localizedDescription);
//         //     Debug.Log(product.metadata.localizedPriceString);
//         // }

//         // assign products
//         products = storeController.products.all
//             .TakeWhile(item => !item.definition.id.Contains("sale"))
//             .OrderBy(item => item.metadata.localizedPrice)
//             .ToList();
//     }

//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         Debug.LogError($"Error initializing IAP because of {error}." + $"\r\nShow a message to the player depending on the error.");
//     }

//     public void OnInitializeFailed(InitializationFailureReason error, string message)
//     {
//         // throw new NotImplementedException();
//     }

//     public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
//     {
//         Debug.Log($"Failed to purchase {product.definition.id} because {failureDescription}");
//     }

//     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
//     {
//         Debug.Log($"Successfully purchased {purchaseEvent.purchasedProduct.definition.id}");

//         var product = purchaseEvent.purchasedProduct;
//         // if (product.definition.id == cItem.id)
//         {
//             AddCoin();
//         }

//         cbPurchaseSuccess?.Invoke();
//         return PurchaseProcessingResult.Complete;
//     }

//     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     {
//         throw new NotImplementedException();
//     }



//     public void BuyCoin()
//     {
//         // storeController.InitiatePurchase(cItem.id);
//     }


//     public void AddCoin()
//     {
//        LocalManager.Instance.AddCoin(50);
//     }

// }