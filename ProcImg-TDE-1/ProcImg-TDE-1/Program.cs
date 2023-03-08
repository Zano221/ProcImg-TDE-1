// See https://aka.ms/new-console-template for more information
using System;

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

    float iCalculatedR = Convert.ToSingle(iR) / 255;
    float iCalculatedG = Convert.ToSingle(iG) / 255;
    float iCalculatedB = Convert.ToSingle(iB) / 255;

    if (iCalculatedR == 0 && iCalculatedG == 0 && iCalculatedB == 0) return (0, 0, 0, 1);

    Console.WriteLine(iCalculatedR + iCalculatedG + iCalculatedB);

    float iK = 1.0f - Math.Max(Math.Max(iCalculatedR, iCalculatedG), iCalculatedB);

    Console.WriteLine("K é = " + iK);

    Console.WriteLine("CONVERTENDO PARA VRAU");
    Console.WriteLine("1 - " + iCalculatedR + " - " + iK + " / " + "(1 - " + iK + ")");
    float iC = (1 - iCalculatedR - iK) / (1 - iK);
    float iM = (1 - iCalculatedG - iK) / (1 - iK);
    float iY = (1 - iCalculatedB - iK) / (1 - iK);

    return (iC * 100, iM * 100, iY * 100, iK * 100);

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
    return (0, 0, 0);
}


String sInput = " ";

while(sInput != "\n")
{
    Console.Write("Insira o valor R: ");
    int iInputR = Convert.ToInt32(Console.ReadLine());

    Console.Write("Insira o valor G: ");
    int iInputG = Convert.ToInt32(Console.ReadLine());

    Console.Write("Insira o valor B: ");
    int iInputB = Convert.ToInt32(Console.ReadLine());


    (iInputR, iInputG, iInputB) = Assert_RGB_8Bits(iInputR, iInputG, iInputB);

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




