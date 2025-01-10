using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomNumberGenerator : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    
    [SerializeField]
    private Button button;
    
    [SerializeField]
    private TextMeshProUGUI txtList;
    
    [SerializeField]
    private TextMeshProUGUI txtResult;

    [SerializeField]
    private TextMeshProUGUI txtError;

    private bool checkInput = false;

    private void Start()
    {
        // Asignar el listener al botón
        button.onClick.AddListener(GenerateRandomNumbers);
        
        // Validar el input para solo permitir números del 1 al 20
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (int.TryParse(value, out int number))
        {
            Debug.Log(number);
            if ((number < 1) || (number > 20)) {
                txtError.text = "El valor esta fuera del rango: 1 - 20";
                checkInput = false;
                Debug.Log("1");
                Debug.Log(checkInput.ToString());
            } else {
                txtError.text = "";
                checkInput = true;
                Debug.Log("2");
                Debug.Log(number);
                Debug.Log(checkInput.ToString());
            }
        }
        else
        {
            inputField.text = "";
                Debug.Log("3");
        }
    }

    private void GenerateRandomNumbers()
    {
        Debug.Log(checkInput.ToString());
        if (string.IsNullOrEmpty(inputField.text) || !checkInput)
            return;

        int count = int.Parse(inputField.text);
        int[] randomNumbers = new int[count];

        // Generar números aleatorios usando un loop
        for(int i = 0; i < count; i++)
        {
            randomNumbers[i] = Random.Range(0, 101);
        }

        // Mostrar los números generados
        txtList.text = "Números generados:\n" + string.Join(", ", randomNumbers);

        // Calcular y mostrar la suma
        int sum = 0;
        for(int i = 0; i < randomNumbers.Length; i++)
        {
            sum += randomNumbers[i];
        }
        txtResult.text = "Suma total: " + sum.ToString();
    }

    private void OnDestroy()
    {
        // Limpiar los listeners
        button.onClick.RemoveListener(GenerateRandomNumbers);
        inputField.onValueChanged.RemoveListener(ValidateInput);
    }
}