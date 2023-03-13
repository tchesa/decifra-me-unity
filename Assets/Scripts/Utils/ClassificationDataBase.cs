using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

#if UNITY_ANDROID
using System.Runtime.Serialization.Formatters.Binary;
#endif

public class ClassificationDataBase : MonoBehaviour
{
	
	#region Singleton Design Pattern
	private static ClassificationDataBase instance = null;
	public static ClassificationDataBase Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (ClassificationDataBase)FindObjectOfType(typeof(ClassificationDataBase));
				if (instance == null)
					instance = (new GameObject("_ClassificationDataBase")).AddComponent<ClassificationDataBase>();
			}
			return instance;
		}
	}
	#endregion
	
	// Vetor que armazena a classificação do resultado das fases
	// Cada indice do vetor representa uma fase
	int[] levelsClassification;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		
		if(!ReadData())
		{
			levelsClassification = new int[9];
			for (int i = 0; i < levelsClassification.Length; i++) levelsClassification[i] = -1;
		}
		
	}
	
	public int GetLevelClassification(int level)
	{
		return levelsClassification[level];
	}
	
	public void SetLevelClassification(int level, int classification)
	{
		if(levelsClassification[level] < classification)
		{
			levelsClassification[level] = classification;
			
			SaveData();
		}
	}
	
	public void ResetDataBase()
	{
		for (int i = 0; i < levelsClassification.Length; i++) levelsClassification[i] = -1;

		SaveData();
	}
	
	bool ReadData()
	{
		#if UNITY_ANDROID
		if (File.Exists(Constants.FileDirectory + "/levelsClassification.bin"))
		{
			BinaryFormatter deserializer = new BinaryFormatter();
			Stream stream = File.OpenRead(Constants.FileDirectory + "/levelsClassification.bin");
			levelsClassification = (int[])deserializer.Deserialize(stream);
			stream.Close();
			return true;
		}
		else
		{
			return false;
		}
#else
		if(File.Exists(Constants.FileDirectory + "/levelsClassification.xml"))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(int[]));
			StreamReader reader = new StreamReader(Constants.FileDirectory + "/levelsClassification.xml");
			
			levelsClassification = (int[])serializer.Deserialize(reader);
			reader.Close();
			return true;
		}
		else
		{
			return false;
		}
#endif
	}
	
	void SaveData()
	{
#if UNITY_ANDROID
		Stream stream = File.Create(Constants.FileDirectory + "/levelsClassification.bin");
		BinaryFormatter serializer = new BinaryFormatter();
		
		serializer.Serialize(stream, levelsClassification);
		stream.Close();
#else		
		XmlSerializer serializer = new XmlSerializer(typeof(int[]));
		StreamWriter writer = new StreamWriter(Constants.FileDirectory + "/levelsClassification.xml");
		
		serializer.Serialize(writer, levelsClassification);
		writer.Close();
#endif
	}
	
	public bool AllLevelsComplete()
	{
		foreach (int i in levelsClassification)
		{
			if (i == -1) return false;
		}
		
		return true;
	}
	
	public bool Complete()
	{
		foreach(int i in levelsClassification)
		{
			if (i < 2) return false;
		}
		return true;
	}
}