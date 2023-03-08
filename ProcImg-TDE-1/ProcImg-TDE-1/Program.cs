// See https://aka.ms/new-console-template for more information

(int, int, int) Assert_RGB_8Bits(int iR, int iG, int iB)
{
    //Maior que 8bitos
    if(iR > 255)
    {
        iR = 255;
    }

    if(iG > 255)
    {
        iG = 255;
    }

    if(iB > 255)
    {
        iB = 255;
    }

    //Menor que 8bitos

    if(iR < 0)
    {
        iR = 0;
    }

    if(iG < 0)
    {
        iG = 0;
    }

    if(iB < 0)
    {
        iB = 0;
    }

    return (iR, iG, iB);
}

float Normalize_RGB(int iR, int iG, int iB)
{
    (iR, iG, iB) = Assert_RGB_8Bits(iR, iG, iB);

    float fCalculatedR = (float)iR / (iR + iG + iB);
    float fCalculatedG = (float)iG / (iR + iG + iB);
    float fCalculatedB = (float)iB / (iR + iG + iB);

    return fCalculatedR + fCalculatedG + fCalculatedB;
}

Console.Write("Insira o valor R: ");
int iInputR = Convert.ToInt32(Console.ReadLine());

Console.Write("Insira o valor G: ");
int iInputG = Convert.ToInt32(Console.ReadLine());

Console.Write("Insira o valor B: ");
int iInputB = Convert.ToInt32(Console.ReadLine());


Console.WriteLine("Valor RGB Normalizado: " + Normalize_RGB(iInputR, iInputG, iInputB).ToString());


