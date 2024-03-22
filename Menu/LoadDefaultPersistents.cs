using UnityEngine;
using System.IO;

public class LoadDefaultPersistents : MonoBehaviour
{
    void Start()
    {
        if (true)//!File.Exists(Application.persistentDataPath + "/CompletedLevel.txt"))
        {
            File.WriteAllText(Application.persistentDataPath + "/CompletedLevel.txt", "");

            string persistentDataPath = Application.persistentDataPath;

            // Create AllSceneRatingsData JSON file
            string allSceneRatingsDataJson = "{\"goldTotal\":0,\"NumberCounting\":\"0\",\"NumberCountingScattered\":\"0\",\"BasicAddition\":\"0\",\"BasicSubtraction\":\"0\",\"ShapePatterns\":\"0\",\"SmallerOrBigger\":\"0\",\"Clock\":\"0\",\"PlaceValues\":\"0\",\"AdditionV\":\"0\",\"AdditionFunctionBox\":\"0\",\"SubtractionFunctionBox\":\"0\",\"NormalAddition\":\"0\",\"NormalSubtraction\":\"0\",\"MultiplicationV\":\"0\",\"DivisionV\":\"0\",\"LongMultiplication\":\"0\",\"FractionFromShape\":\"0\",\"FractionEqualize\":\"0\",\"FractionEqualizeHard\":\"0\",\"LongDivision\":\"0\"}";
            WriteJsonFile(persistentDataPath, "AllSceneRatingsData.json", allSceneRatingsDataJson);

            // Create VariableData JSON file
            string variableDataJson = "{\"currentScene\":\"NumberCounting\",\"counterScene\":0,\"timeElapsed\":0}";
            WriteJsonFile(persistentDataPath, "VariableData.json", variableDataJson);

            string NumberCountingJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":10,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "NumberCounting.json", NumberCountingJson);
            string NumberCountingScatteredJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "NumberCountingScattered.json", NumberCountingScatteredJson);
            string BasicAdditionVJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "BasicAdditionV.json", BasicAdditionVJson);
            string BasicSubtractionVJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "BasicSubtractionV.json", BasicSubtractionVJson);
            string ShapePatternsJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "ShapePatterns.json", ShapePatternsJson);
            string SmallerOrBiggerJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":6}";
            WriteJsonFile(persistentDataPath, "SmallerOrBigger.json", SmallerOrBiggerJson);
            string PlaceValuesJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "PlaceValues.json", PlaceValuesJson);
            string ClockJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "Clock.json", ClockJson);
            string AdditionVJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "AdditionV.json", AdditionVJson);
            string AdditionFunctionBoxJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":4}";
            WriteJsonFile(persistentDataPath, "AdditionFunctionBox.json", AdditionFunctionBoxJson);
            string SubtractionFunctionBoxJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":4}";
            WriteJsonFile(persistentDataPath, "SubtractionFunctionBox.json", SubtractionFunctionBoxJson);
            string NormalAdditionJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":4}";
            WriteJsonFile(persistentDataPath, "NormalAddition.json", NormalAdditionJson);
            string NormalSubtractionJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":4}";
            WriteJsonFile(persistentDataPath, "NormalSubtraction.json", NormalSubtractionJson);
            string MultiplicationVJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "MultiplicationV.json", MultiplicationVJson);
            string DivisionVJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "DivisionV.json", DivisionVJson);
            string LongMultiplicationJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":4}";
            WriteJsonFile(persistentDataPath, "LongMultiplication.json", LongMultiplicationJson);
            string FractionFromShapeJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "FractionFromShape.json", FractionFromShapeJson);
            string FractionEqualizeJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "FractionEqualize.json", FractionEqualizeJson);
            string FractionEqualizeHardJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":5}";
            WriteJsonFile(persistentDataPath, "FractionEqualizeHard.json", FractionEqualizeHardJson);
            string LongDivisionJson = "{\"bestTime\":600,\"bestRating\":\"\",\"goldTime\":15,\"perfTime\":5,\"numRepetitions\":4}";
            WriteJsonFile(persistentDataPath, "LongDivision.json", LongDivisionJson);
            
            Debug.Log("Files Created");
        }
    }
    void WriteJsonFile(string path, string fileName, string jsonContent)
    {
        string filePath = Path.Combine(path, fileName);
        File.WriteAllText(filePath, jsonContent);
    }
}