// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using UnityEngine.UI;

// public class LoadImage : MonoBehaviour {
//     [HeaderAttribute("Example : 'C:/bjb'")]
//     public string editorFilesLocation = "D:/BhaktiUGP/Assets/Download/bjb";
//     public string standAloneFilesLocation = "/StreamingAssets/bjb";
//     [SpaceAttribute(10f)]
//     public List<Texture2D> images = new List<Texture2D>();
//     public RawImage rw;
//     public ManagerData md;
//     public RawImage[] imgIsian = new RawImage[9];
//     public Text[] txtIsiStock = new Text[9];
//     public Image[] imgStar = new Image[3];
//     public Sprite spawal, spganti;
//     public Image imgStarbanget;

//     [HeaderAttribute("Current")]
//     [SerializeField] string path = "";
//     IEnumerator mulai;

//     private void Start()
//     {
//         #if UNITY_EDITOR
//             path = editorFilesLocation;
//         #elif UNITY_STANDALONE
//             path = Application.dataPath + standAloneFilesLocation;
//         #endif

//         if (PlayerPrefs.GetInt("start") == 0)
//         {
//             for (int a=0; a<imgIsian.Length; a++)
//             {
//                 PlayerPrefs.SetInt((a + 1).ToString(), 0);
//             }
//         }

//         Init();
//     }

//     public void Init()
//     {
//         // StartCoroutine(Mulai());
//         mulai = Mulai();
//         StartCoroutine(Mulai());
//     }

//     public IEnumerator Mulai()
//     {
//         yield return StartCoroutine(
//             "LoadAll",
//             Directory.GetFiles(path, "*.png", SearchOption.AllDirectories)
//         );

//         mulai = null;
//     }

//     public IEnumerator LoadAll(string[] filePaths)
//     {
//         foreach (string filePath in filePaths)
//         {
//             WWW load = new WWW("file:///" + filePath);
//             yield return load;
//             if (!string.IsNullOrEmpty(load.error))
//             {
//                 Debug.LogWarning(filePath + " error");
//             }
//             else
//             {
//                 images.Add(load.texture);
//             }

//             Debug.Log("Load images");
//         }
//     }
//     private void Update()
//     {
//         if (mulai != null)
//         {
//             Debug.Log("Still Load Images, Return");
//             return;
//         }

//         rw.texture = images[md.a];
//         for(int a=0; a<imgIsian.Length; a++)
//         {
//             imgIsian[a].texture = images[a];
//             txtIsiStock[a].text = PlayerPrefs.GetInt((a + 1).ToString()).ToString();
//         }
//         if(md.a == 1)
//         {
//             if (PlayerPrefs.GetInt("jackpot1") == 0)
//             {
//                 imgStarbanget.sprite = spawal;
//             }
//             else
//             {
//                 imgStarbanget.sprite = spganti;
//             }
//         }else if (md.a == 4)
//         {
//             if (PlayerPrefs.GetInt("jackpot2") == 0)
//             {
//                 imgStarbanget.sprite = spawal;
//             }
//             else
//             {
//                 imgStarbanget.sprite = spganti;
//             }
//         }else if (md.a == 7)
//         {
//             if (PlayerPrefs.GetInt("jackpot3") == 0)
//             {
//                 imgStarbanget.sprite = spawal;
//             }
//             else
//             {
//                 imgStarbanget.sprite = spganti;
//             }
//         }

//         if (PlayerPrefs.GetInt("jackpot1") == 0)
//         {
//             imgStar[0].sprite = spawal;
//         }
//         else
//         {
//             imgStar[0].sprite = spganti;
//         }
//         if (Input.GetKeyDown(KeyCode.Q))
//         {
//             if (PlayerPrefs.GetInt("jackpot1") == 0)
//             {
//                 PlayerPrefs.SetInt("jackpot1", 1);
//             }
//             else if (PlayerPrefs.GetInt("jackpot1") != 0)
//             {
//                 PlayerPrefs.SetInt("jackpot1", 0);
//             }
//         }


//         //jackpot2
//         if (PlayerPrefs.GetInt("jackpot2") == 0)
//         {
//             imgStar[1].sprite = spawal;
//         }
//         else
//         {
//             imgStar[1].sprite = spganti;
//         }
//         if (Input.GetKeyDown(KeyCode.W))
//         {
//             if (PlayerPrefs.GetInt("jackpot2") == 0)
//             {
//                 PlayerPrefs.SetInt("jackpot2", 1);
//             }
//             else if (PlayerPrefs.GetInt("jackpot2") != 0)
//             {
//                 PlayerPrefs.SetInt("jackpot2", 0);
//             }
//         }


//         //jackpot3
//         if (PlayerPrefs.GetInt("jackpot3") == 0)
//         {
//             imgStar[2].sprite = spawal;
//         }
//         else
//         {
//             imgStar[2].sprite = spganti;
//         }
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             if (PlayerPrefs.GetInt("jackpot3") == 0)
//             {
//                 PlayerPrefs.SetInt("jackpot3", 1);
//             }
//             else if (PlayerPrefs.GetInt("jackpot3") != 0)
//             {
//                 PlayerPrefs.SetInt("jackpot3", 0);
//             }
//         }

//         if(md.a == 1 || md.a==4 || md.a == 7)
//         {
//             imgStarbanget.gameObject.SetActive(true);
            
//         }else
//         {
//             imgStarbanget.gameObject.SetActive(false);
//         }
//     }
//     public void startApplication()
//     {
//         PlayerPrefs.SetInt("start", 1);
//     }

//     public void AktifkanBlock()
//     {
//         if (md.a == 1)
//         {
//             if (PlayerPrefs.GetInt("jackpot1") == 0)
//             {
//                 PlayerPrefs.SetInt("jackpot1", 1);
//             }
//             else
//             {
//                 PlayerPrefs.SetInt("jackpot1", 0);
//             }
//         }
//         else if (md.a == 4)
//         {
//             if (PlayerPrefs.GetInt("jackpot2") == 0)
//             {
//                 PlayerPrefs.SetInt("jackpot2", 1);
//             }
//             else
//             {
//                 PlayerPrefs.SetInt("jackpot2", 0);
//             }
//         }
//         else if (md.a == 7)
//         {
//             if (PlayerPrefs.GetInt("jackpot3") == 0)
//             {
//                 PlayerPrefs.SetInt("jackpot3", 1);
//             }
//             else
//             {
//                 PlayerPrefs.SetInt("jackpot3", 0);
//             }
//         }
//     }
// }
