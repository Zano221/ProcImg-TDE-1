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

(float , float, float, float) Convert_RGB_CMYK(int iR, int iG, int iB)
{

    float fCalculatedR = Convert.ToSingle(iR) / 255;
    float fCalculatedG = Convert.ToSingle(iG) / 255;
    float fCalculatedB = Convert.ToSingle(iB) / 255;

    if (fCalculatedR == 0 && fCalculatedG == 0 && fCalculatedB == 0) return (0, 0, 0, 1);

    float fK = 1.0f - Math.Max(Math.Max(fCalculatedR, fCalculatedG), fCalculatedB);

    

    Console.WriteLine("K é = " + fK);

    Console.WriteLine("CONVERTENDO PARA VRAU");
    Console.WriteLine("1 - " + fCalculatedR + " - " + fK + " / " + "(1 - " + fK + ")");
    float iC = (1 - fCalculatedR - fK) / (1 - fK);
    float iM = (1 - fCalculatedG - fK) / (1 - fK);
    float iY = (1 - fCalculatedB - fK) / (1 - fK);

    return (iC * 100, iM * 100, iY * 100, fK * 100);

}

(int, int, int) Convert_CMYK_RGB(int iC, int iM, int iY, int iK)
{
    int iR = 255 * (1 - iC) * (1 - iK);
    int iG = 255 * (1 - iM) * (1 - iK);
    int iB = 255 * (1 - iY) * (1 - iK);

    return (iR, iG, iB);
}


(float, float, float) Convert_RGB_HSV(int iR, int iG, int iB)
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

    return (fH, fS, fV);
}


bool bIsInput = true;

while(true)
{
    int iInputR = 0, iInputG = 0, iInputB = 0;
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
    catch(Exception e)
    {
        Console.WriteLine("\n" +
            "   --- ERRO!, o valor inserido não é numerico! --- \n");
        bIsInput = false;
    }

    if(!bIsInput)
    {
        return;
    }

    Console.WriteLine("");
    Console.WriteLine("Valor RGB Normalizado: " + Normalize_RGB(iInputR, iInputG, iInputB).ToString());

    String sCOutput;
    String sYOutput;
    String sMOutput;
    String sKOutput;

    float fCOutput;
    float fYOutput;
    float fMOutput;
    float fKOutput;


    (fCOutput, fYOutput, fMOutput, fKOutput) = Convert_RGB_CMYK(iInputR, iInputG, iInputB);

    sCOutput = fCOutput.ToString("f1");
    sYOutput = fYOutput.ToString("f1");
    sMOutput = fMOutput.ToString("f1");
    sKOutput = fKOutput.ToString("f1");


    Console.WriteLine("Valor RGB Convertido para CMYK: (" + sCOutput + "% , " + sYOutput + "% , " +  sMOutput + "% , " + sKOutput + "%)" );

    Console.WriteLine("Valor RGB Convertido para HSV: ");
}







