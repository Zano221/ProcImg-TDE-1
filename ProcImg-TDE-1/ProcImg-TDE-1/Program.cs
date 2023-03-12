// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;

(int, int, int) Assert_RGB_8Bits(int iR, int iG, int iB)
{
    //Maior que 8bitos
    if(iR > 255) iR = 255;
    if(iG > 255) iG = 255;
    if(iB > 255) iB = 255;

    //Menor que 8bitos
    if(iR < 0) iR = 0;
    if(iG < 0) iG = 0;
    if(iB < 0) iB = 0;

    return (iR, iG, iB);
}

(int, int, int, int) Assert_CMYK_100(int iC, int iM, int iY, int iK)
{
    //Maior que 100%
    if (iC > 100) iC = 100;
    if (iM > 100) iM = 100;
    if (iY > 100) iY = 100;
    if (iK > 100) iK = 100;

    //Menor que 0%
    if (iC < 0) iC = 0;
    if (iM < 0) iM = 0;
    if (iY < 0) iY = 0;
    if (iK < 0) iK = 0;

    return (iC, iM, iY, iK);
}

(int, int, int) Assert_HSV_100(int iH, int iS, int iV)
{
    //Maior que 100%
    if (iH > 100) iH = 100;
    if (iS > 100) iS = 100;
    if (iV > 100) iV = 100;

    //Menor que 0%
    if (iH < 0) iH = 0;
    if (iS < 0) iS = 0;
    if (iV < 0) iV = 0;

    return (iH, iS, iV);
}

float Calc_Delta(int iR, int iG, int iB)
{
    float iCalculatedR = Convert.ToSingle(iR) / 255;
    float iCalculatedG = Convert.ToSingle(iG) / 255;
    float iCalculatedB = Convert.ToSingle(iB) / 255;

    float fMax = Math.Max(Math.Max(iCalculatedR, iCalculatedG), iCalculatedB);
    float fMin = Math.Min(Math.Min(iCalculatedR, iCalculatedG), iCalculatedB);

    return fMax - fMin;
}

float Normalize_RGB(int iR, int iG, int iB)
{
    if (iR == 0 && iG == 0 && iB == 0) return 0;

    float fR = Convert.ToSingle(iR);
    float fG = Convert.ToSingle(iG);
    float fB = Convert.ToSingle(iB);

    float fCalculatedR = fR / (fR + fG + fB);
    float fCalculatedG = fG / (fR + fG + fB);
    float fCalculatedB = fB / (fR + fG + fB);

    return fCalculatedR + fCalculatedG + fCalculatedB;
}

(int ,int , int, int) Convert_RGB_CMYK(int iR, int iG, int iB)
{

    float fCalculatedR = Convert.ToSingle(iR) / 255;
    float fCalculatedG = Convert.ToSingle(iG) / 255;
    float fCalculatedB = Convert.ToSingle(iB) / 255;

    if (fCalculatedR == 0 && fCalculatedG == 0 && fCalculatedB == 0) return (0, 0, 0, 1);

    float fK = 1.0f - Math.Max(Math.Max(fCalculatedR, fCalculatedG), fCalculatedB);

    float fC = (1 - fCalculatedR - fK) / (1 - fK);
    float fM = (1 - fCalculatedG - fK) / (1 - fK);
    float fY = (1 - fCalculatedB - fK) / (1 - fK);

    return (Convert.ToInt32(Math.Ceiling(fC * 100)),
        Convert.ToInt32(Math.Ceiling(fM * 100)),
        Convert.ToInt32(Math.Ceiling(fY * 100)),
        Convert.ToInt32(Math.Ceiling(fK * 100)));

}

(int, int, int) Convert_CMYK_RGB(int iC, int iM, int iY, int iK)
{
    float fC = Convert.ToSingle(iC) / 100;
    float fM = Convert.ToSingle(iM) / 100;
    float fY = Convert.ToSingle(iY) / 100;
    float fK = Convert.ToSingle(iK) / 100;



    float fR = Convert.ToSingle(Math.Ceiling(255 * (1 - fC) * (1 - fK)));
    float fG = Convert.ToSingle(Math.Ceiling(255 * (1 - fM) * (1 - fK)));
    float fB = Convert.ToSingle(Math.Ceiling(255 * (1 - fY) * (1 - fK)));

    int iR = Convert.ToInt32(fR);
    int iG = Convert.ToInt32(fG);
    int iB = Convert.ToInt32(fB);

    return (iR, iG, iB);
}


(int, int, int) Convert_RGB_HSV(int iR, int iG, int iB)
{
    float fDelta = Calc_Delta(iR, iG, iB);

    float fCalculatedR = Convert.ToSingle(iR) / 255;
    float fCalculatedG = Convert.ToSingle(iG) / 255;
    float fCalculatedB = Convert.ToSingle(iB) / 255;


    float fMax = Math.Max(Math.Max(fCalculatedR, fCalculatedG), fCalculatedB);
    float fMin = Math.Min(Math.Min(fCalculatedR, fCalculatedG), fCalculatedB);

    float fH = 0;


    //Calcular o Hue
    if (fMax == fCalculatedR)
    {
        if(fCalculatedG >= fCalculatedB)
        {
            fH = 60 * (fCalculatedG - fCalculatedB) / fDelta;
        }
        else if(fCalculatedG < fCalculatedB)
        {
            fH = 60 * ((fCalculatedG - fCalculatedB) / fDelta) + 360;
        }
    }
    else if(fMax == fCalculatedG)
    {
        fH = 60 * ((fCalculatedB - fCalculatedR) / fDelta) + 120;
    }
    else if(fMax == fCalculatedB)
    {
        fH = 60 * ((fCalculatedR - fCalculatedG) / fDelta) + 240;
    }

    //Calcular a Saturação
    float fS = fDelta / fMax;

    //Calcular o Valor
    float fV = fMax;

    return (Convert.ToInt32(Math.Floor(fH)), 
        Convert.ToInt32(Math.Ceiling(fS * 100)), 
        Convert.ToInt32(Math.Ceiling(fV * 100)));
}

void Convert_HSV_RGB(int iH, int iS, int iV)
{
    float fH = Convert.ToSingle(iH) / 100.0f;
    float fS = Convert.ToSingle(iS) / 100.0f;
    float fV = Convert.ToSingle(iV) / 100.0f;

    int iC = iS * iV;
    int iX = iC * (1 - Math.Abs((iH / 60) % 2 - 1));

}

bool bIsInput = true;

while(true)
{
    bool bIsInputValid = true;

    Console.WriteLine("\nInsira o numero da entrada correspondente (em numeros)\n");
    Console.WriteLine(" - (1). RGB (normalizado, convertido para CMYK, HSV, e escala de cinza)");
    Console.WriteLine(" - (2). CMYK para RGB");
    Console.WriteLine(" - (3). HSV para RGB");
    
    String sInput = Console.ReadLine();
    int iChoice = 0;

    try
    {
        iChoice = Convert.ToInt32(sInput);
    }
    catch(Exception e)
    {
        Console.Write("\nERRO, OCORREU UM ERRO: ");

        if(e.HResult == -2146233033)
        {
            Console.WriteLine("A ENTRADA DEVE SER EM NUMEROS (ENTRE 1-3)");
        }
        else
        {
            Console.WriteLine(e.Message);
        }

        bIsInputValid = false;
    }

    if(!bIsInputValid) 
    {
        return;
    }

    if(iChoice < 1 || iChoice > 3)
    {
        Console.WriteLine("ERRO! O VALOR DE CONVERSÃO DEVE SER ENTRE 1-3");
        break;
    }

    int iInputR = 0, iInputG = 0, iInputB = 0;
    int iInputC = 0, iInputM = 0, iInputY = 0, iInputK = 0;
    int iInputH = 0, iInputS = 0, iInputV = 0;

    //Tudo relacionado a RGB
    if(iChoice == 1) 
    {
        try
        {
            Console.Write("Insira o valor R: ");
            iInputR = Convert.ToInt32(Console.ReadLine());

            Console.Write("Insira o valor G: ");
            iInputG = Convert.ToInt32(Console.ReadLine());

            Console.Write("Insira o valor B: ");
            iInputB = Convert.ToInt32(Console.ReadLine());

            (iInputR, iInputG, iInputB) = Assert_RGB_8Bits(iInputR, iInputG, iInputB);
        }
        catch (Exception e)
        {
            Console.WriteLine("\n" +
                "   --- ERRO!, o valor inserido não é numerico! --- \n");
            bIsInput = false;
        }

        if (!bIsInput)
        {
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Valor RGB Normalizado: " + Normalize_RGB(iInputR, iInputG, iInputB).ToString());

        float fCOutput;
        float fYOutput;
        float fMOutput;
        float fKOutput;

        (fCOutput, fYOutput, fMOutput, fKOutput) = Convert_RGB_CMYK(iInputR, iInputG, iInputB);

        Console.WriteLine("Valor RGB Convertido para CMYK: " +
            "(" + fCOutput.ToString() + "% , " + fYOutput.ToString() + "% , " + fMOutput.ToString() + "% , " + fKOutput.ToString() + "%)");
        Console.WriteLine("Valor RGB Convertido para HSV: " + Convert_RGB_HSV(iInputR, iInputG, iInputB).ToString());
    }
    //Tudo relacionado a CMYK
    else if (iChoice == 2)
    {
        try
        {
            Console.Write("Insira o valor C: ");
            iInputC = Convert.ToInt32(Console.ReadLine());

            Console.Write("Insira o valor M: ");
            iInputM = Convert.ToInt32(Console.ReadLine());

            Console.Write("Insira o valor Y: ");
            iInputY = Convert.ToInt32(Console.ReadLine());

            Console.Write("Insira o valor K: ");
            iInputK = Convert.ToInt32(Console.ReadLine());

        }
        catch(Exception e)
        {
            Console.WriteLine("\n" +
                "   --- ERRO!, o valor inserido não é numerico! --- \n");
            bIsInput = false;
        }

        (iInputC, iInputM, iInputY, iInputK) = Assert_CMYK_100(iInputC, iInputM, iInputY, iInputK);


        Console.WriteLine("Valor CMYK Convertido para RGB: " + Convert_CMYK_RGB(iInputC, iInputM, iInputY, iInputK));
    }
    //Tudo relacionado a HSV
    else if (iChoice == 3)
    {
        try
        {
            Console.WriteLine("Insira o valor H: ");
            iInputH = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insira o valor S: ");
            iInputH = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Insira o valor V: ");
            iInputH = Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("\n" +
                "   --- ERRO!, o valor inserido não é numerico! --- \n");
            bIsInput = false;
        }


        (iInputH, iInputS, iInputV) = Assert_HSV_100(iInputH, iInputS, iInputV);

        Console.WriteLine("Valor HSV Convertido para RGB: " + Convert_HSV_RGB(iInputH, iInputS, iInputV));
    }
    //Vai dar erro e pedir para reinserir os dados
    else
    {

    }

    Console.WriteLine("\nPressione qualquer botão para prosseguir\n");
    Console.Read();
    Console.Clear();
}







