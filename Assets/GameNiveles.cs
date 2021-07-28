using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNiveles : MonoBehaviour
{
    public List<Color> colores;
    public List<Level> niveles;

    public void Start()
    {
        niveles = new List<Level>();
        niveles.Add(new Level(0, "423")); //nivel 0/*
        niveles.Add(new Level(1, "1356")); //nivel 10
        niveles.Add(new Level(2, "12345")); //nivel 21
        niveles.Add(new Level(3, "VINCE")); //nivel 37
        niveles.Add(new Level(4, "EZPERO")); //nivel 46
        niveles.Add(new Level(5, "POIDSA8")); //nivel 58
        niveles.Add(new Level(6, "3EDC5TGB")); //nivel 73
        niveles.Add(new Level(7, "QRY1467")); //nivel 88
        niveles.Add(new Level(8, "1E2R3T45")); //nivel 103
        niveles.Add(new Level(9, "QWERTYUIOP")); //nivel 117
        niveles.Add(new Level(10, "123456789")); //nivel 135
        niveles.Add(new Level(11, "2X3C4V5B6N7")); //nivel 152
        niveles.Add(new Level(12, "VZAFRQ1425TW")); //nivel 167
        niveles.Add(new Level(13, "1AZ2SX3DC4FV5GB")); //nivel 185
        niveles.Add(new Level(0, "213")); //nivel 1
        niveles.Add(new Level(0, "789")); //nivel 2
        niveles.Add(new Level(0, "IJK")); //nivel 3
        niveles.Add(new Level(0, "STU")); //nivel 4
        niveles.Add(new Level(0, "1Z3")); //nivel 5
        niveles.Add(new Level(0, "RST")); //nivel 6
        niveles.Add(new Level(0, "DEF")); //nivel 7
        niveles.Add(new Level(0, "456")); //nivel 8
        niveles.Add(new Level(0, "PQR")); //nivel 9
        niveles.Add(new Level(1, "1356")); //nivel 10
        niveles.Add(new Level(1, "6321")); //nivel 11
        niveles.Add(new Level(1, "4327")); //nivel 12
        niveles.Add(new Level(1, "1723")); //nivel 13
        niveles.Add(new Level(1, "ANST")); //nivel 14
        niveles.Add(new Level(1, "S23E")); //nivel 15
        niveles.Add(new Level(1, "STUV")); //nivel 16
        niveles.Add(new Level(1, "9ABC")); //nivel 17
        niveles.Add(new Level(1, "NOPQ")); //nivel 18
        niveles.Add(new Level(1, "1234")); //nivel 19
        niveles.Add(new Level(1, "JKLM")); //nivel 20
        niveles.Add(new Level(2, "12345")); //nivel 21
        niveles.Add(new Level(2, "789AB")); //nivel 22
        niveles.Add(new Level(2, "IJKLM")); //nivel 23
        niveles.Add(new Level(2, "STUVW")); //nivel 24
        niveles.Add(new Level(2, "65432")); //nivel 25
        niveles.Add(new Level(2, "DEFGH")); //nivel 26
        niveles.Add(new Level(2, "IKST3")); //nivel 27
        niveles.Add(new Level(2, "1XY7B")); //nivel 28
        niveles.Add(new Level(2, "786PQ")); //nivel 29
        niveles.Add(new Level(2, "WVUT4")); //nivel 30
        niveles.Add(new Level(3, "1G2H3")); //nivel 31
        niveles.Add(new Level(3, "54321")); //nivel 32
        niveles.Add(new Level(3, "ABC3P")); //nivel 33
        niveles.Add(new Level(3, "PABLO")); //nivel 34
        niveles.Add(new Level(3, "1996K")); //nivel 35
        niveles.Add(new Level(3, "NATUS")); //nivel 36
        niveles.Add(new Level(3, "VINCE")); //nivel 37
        niveles.Add(new Level(3, "RES8A")); //nivel 38
        niveles.Add(new Level(3, "VI2IL")); //nivel 39
        niveles.Add(new Level(3, "HOLA1")); //nivel 40
        niveles.Add(new Level(4, "TUYHI1")); //nivel 41
        niveles.Add(new Level(4, "ALO986")); //nivel 42
        niveles.Add(new Level(4, "CARAJO")); //nivel 43
        niveles.Add(new Level(4, "123456")); //nivel 44
        niveles.Add(new Level(4, "2468DI")); //nivel 45
        niveles.Add(new Level(4, "EZPERO")); //nivel 46
        niveles.Add(new Level(4, "RATATA")); //nivel 47
        niveles.Add(new Level(4, "13452A")); //nivel 48
        niveles.Add(new Level(4, "IUPAC1")); //nivel 49
        niveles.Add(new Level(4, "TENEMO")); //nivel 50
        niveles.Add(new Level(4, "1956DF")); //nivel 51
        niveles.Add(new Level(4, "BRAZIL")); //nivel 52
        niveles.Add(new Level(4, "REQUEN")); //nivel 53
        niveles.Add(new Level(4, "1586KA")); //nivel 54
        niveles.Add(new Level(4, "AIU78D")); //nivel 55
        niveles.Add(new Level(5, "ASDYUI1")); //nivel 56
        niveles.Add(new Level(5, "QWEJKLT")); //nivel 57
        niveles.Add(new Level(5, "POIDSA8")); //nivel 58
        niveles.Add(new Level(5, "QZECTBJ")); //nivel 59
        niveles.Add(new Level(5, "1357964")); //nivel 60
        niveles.Add(new Level(5, "7654321")); //nivel 61
        niveles.Add(new Level(5, "POIUYTR")); //nivel 62
        niveles.Add(new Level(5, "ASDFGHJ")); //nivel 63
        niveles.Add(new Level(5, "LKJHGFD")); //nivel 64
        niveles.Add(new Level(5, "ZXCVBNM")); //nivel 65
        niveles.Add(new Level(5, "TGEDQA1")); //nivel 66
        niveles.Add(new Level(5, "WSRFYHI")); //nivel 67
        niveles.Add(new Level(5, "97531OU")); //nivel 68
        niveles.Add(new Level(5, "1QA3ED4")); //nivel 69
        niveles.Add(new Level(5, "2WS4RF6")); //nivel 70
        niveles.Add(new Level(6, "1QAZ3EDC")); //nivel 71
        niveles.Add(new Level(6, "2WSX4RFV")); //nivel 72
        niveles.Add(new Level(6, "3EDC5TGB")); //nivel 73
        niveles.Add(new Level(6, "4RFV6YHN")); //nivel 74
        niveles.Add(new Level(6, "MNBVFDSA")); //nivel 75
        niveles.Add(new Level(6, "QWERFGHJ")); //nivel 76
        niveles.Add(new Level(6, "0987POIU")); //nivel 77
        niveles.Add(new Level(6, "ABCDEFGH")); //nivel 78
        niveles.Add(new Level(6, "IJKLM123")); //nivel 79
        niveles.Add(new Level(6, "1ETH3680")); //nivel 80
        niveles.Add(new Level(6, "CHOXFIQW")); //nivel 81
        niveles.Add(new Level(6, "1579QRYI")); //nivel 82
        niveles.Add(new Level(6, "AFHKQETU")); //nivel 83
        niveles.Add(new Level(6, "6432HGFD")); //nivel 84
        niveles.Add(new Level(6, "BNMTYU3A")); //nivel 85
        niveles.Add(new Level(7, "A5ERTPI")); //nivel 86
        niveles.Add(new Level(7, "KLGDSAN")); //nivel 87
        niveles.Add(new Level(7, "QRY1467")); //nivel 88
        niveles.Add(new Level(7, "HFSNVXG")); //nivel 89
        niveles.Add(new Level(7, "ZXCASDQ")); //nivel 90
        niveles.Add(new Level(7, "XCVSDFE")); //nivel 91
        niveles.Add(new Level(7, "CVBDFGR")); //nivel 92
        niveles.Add(new Level(7, "VBNFGHT")); //nivel 93
        niveles.Add(new Level(7, "BNMGHJY")); //nivel 94
        niveles.Add(new Level(7, "IUYKJHO")); //nivel 95
        niveles.Add(new Level(7, "GFDTRE4")); //nivel 96
        niveles.Add(new Level(7, "765UYT4")); //nivel 97
        niveles.Add(new Level(7, "ADGQETY")); //nivel 98
        niveles.Add(new Level(7, "AFJQRU4")); //nivel 99
        niveles.Add(new Level(7, "YRWHFSG")); //nivel 100
        niveles.Add(new Level(8, "13567892")); //nivel 101
        niveles.Add(new Level(8, "TRE54312")); //nivel 102
        niveles.Add(new Level(8, "1E2R3T45")); //nivel 103
        niveles.Add(new Level(8, "ETQRADFG")); //nivel 104
        niveles.Add(new Level(8, "12345678")); //nivel 105
        niveles.Add(new Level(8, "QWERTYUI")); //nivel 106
        niveles.Add(new Level(8, "ASDFGHJK")); //nivel 107
        niveles.Add(new Level(8, "ZXCVBNML")); //nivel 108
        niveles.Add(new Level(8, "PIYRWADG")); //nivel 109
        niveles.Add(new Level(8, "ZCBADGJL")); //nivel 110
        niveles.Add(new Level(8, "MBCXZJFS")); //nivel 111
        niveles.Add(new Level(8, "UTE753JG")); //nivel 112
        niveles.Add(new Level(8, "5TGB6YHN")); //nivel 113
        niveles.Add(new Level(8, "2WSX3EDC")); //nivel 114
        niveles.Add(new Level(8, "13579QET")); //nivel 115
        niveles.Add(new Level(9, "1234567890")); //nivel 116
        niveles.Add(new Level(9, "QWERTYUIOP")); //nivel 117
        niveles.Add(new Level(9, "ASDFGHJKLM")); //nivel 118
        niveles.Add(new Level(9, "ZXCVBNMJHG")); //nivel 119
        niveles.Add(new Level(9, "ASDFGQWERT")); //nivel 120
        niveles.Add(new Level(9, "HJKLYUIO78")); //nivel 121
        niveles.Add(new Level(9, "65432NBVCX")); //nivel 122
        niveles.Add(new Level(9, "XSW2CDE34R")); //nivel 123
        niveles.Add(new Level(9, "FV5TGB6YNH")); //nivel 124
        niveles.Add(new Level(9, "MJU7654321")); //nivel 125
        niveles.Add(new Level(9, "GFDSAUYTRE")); //nivel 126
        niveles.Add(new Level(9, "NBVCXHGFDS")); //nivel 127
        niveles.Add(new Level(9, "POIUY09876")); //nivel 128
        niveles.Add(new Level(9, "JHGFDFUYT82")); //nivel 129
        niveles.Add(new Level(9, "1M2N3B4C5X")); //nivel 130
        niveles.Add(new Level(10, "AHSGDFQTW")); //nivel 131
        niveles.Add(new Level(10, "123456789")); //nivel 132
        niveles.Add(new Level(10, "YTREWQFDS")); //nivel 133
        niveles.Add(new Level(10, "ZXCVBNMAS")); //nivel 134
        niveles.Add(new Level(10, "DFGHJKLQW")); //nivel 135
        niveles.Add(new Level(10, "7654321EQ")); //nivel 136
        niveles.Add(new Level(10, "QWERTYUIO")); //nivel 137
        niveles.Add(new Level(10, "123RTYHJK")); //nivel 138
        niveles.Add(new Level(10, "1DWF3GTCD")); //nivel 139
        niveles.Add(new Level(10, "KGSAUTEQ5")); //nivel 140
        niveles.Add(new Level(10, "13579ADGJ")); //nivel 141
        niveles.Add(new Level(10, "MBCZJGDTE")); //nivel 142
        niveles.Add(new Level(10, "3FRGYJSQA")); //nivel 143
        niveles.Add(new Level(10, "AMGHFD5YJ")); //nivel 144
        niveles.Add(new Level(10, "ACSVDBFN4")); //nivel 145
        niveles.Add(new Level(11, "1S2D3F4G5HT")); //nivel 146
        niveles.Add(new Level(11, "GDATEQ5316H")); //nivel 147
        niveles.Add(new Level(11, "1234567890A")); //nivel 148
        niveles.Add(new Level(11, "BCDEFGHYUIO")); //nivel 149
        niveles.Add(new Level(11, "DSAHGFLKJUT")); //nivel 150
        niveles.Add(new Level(11, "EQ3164YR97I")); //nivel 151
        niveles.Add(new Level(11, "2X3C4V5B6N7")); //nivel 152
        niveles.Add(new Level(11, "M7N6FDSAQWE")); //nivel 153
        niveles.Add(new Level(11, "MBCZJGDAY3Q")); //nivel 154
        niveles.Add(new Level(11, "RWQFSAVXZMJ")); //nivel 155
        niveles.Add(new Level(11, "15243QTWRED")); //nivel 156
        niveles.Add(new Level(11, "ZBXVCAGSFDY")); //nivel 157
        niveles.Add(new Level(11, "MCNVJDHFUE1")); //nivel 158
        niveles.Add(new Level(11, "1S2D3F4G5H6")); //nivel 159
        niveles.Add(new Level(11, "6421TEQFSCX")); //nivel 160
        niveles.Add(new Level(12, "1A2S3D4F5G6E")); //nivel 161
        niveles.Add(new Level(12, "CXZDSAEWQ321")); //nivel 162
        niveles.Add(new Level(12, "135QETADGZCB")); //nivel 163
        niveles.Add(new Level(12, "1234567890QW")); //nivel 164
        niveles.Add(new Level(12, "321YTRDSANBV")); //nivel 165
        niveles.Add(new Level(12, "XVNSFHDGJETU")); //nivel 166
        niveles.Add(new Level(12, "VZAFRQ1425TW")); //nivel 167
        niveles.Add(new Level(12, "MBJGUT75CZDA")); //nivel 168
        niveles.Add(new Level(12, "DSAEWQ5TG6YH")); //nivel 169
        niveles.Add(new Level(12, "3ED4RF5TG6YH")); //nivel 170
        niveles.Add(new Level(12, "EQTDAGCZBFSV")); //nivel 171
        niveles.Add(new Level(12, "321456EWQRTY")); //nivel 172
        niveles.Add(new Level(12, "JHGSDFUYTWER")); //nivel 173
        niveles.Add(new Level(12, "765123EWQADG")); //nivel 174
        niveles.Add(new Level(12, "ZCBADGHFSYRW")); //nivel 175
        niveles.Add(new Level(13, "MBCZJGDATEQ5314")); //nivel 176
        niveles.Add(new Level(13, "1234567890QWERT")); //nivel 177
        niveles.Add(new Level(13, "ZXCVASDFQWER123")); //nivel 178
        niveles.Add(new Level(13, "654YTRHGFNBVDSA")); //nivel 179
        niveles.Add(new Level(13, "321654YTRHGFDSA")); //nivel 180
        niveles.Add(new Level(13, "CXZDSANBVHGFYTR")); //nivel 181
        niveles.Add(new Level(13, "135QETADGZCB678")); //nivel 182
        niveles.Add(new Level(13, "1QA2WS3ED4RF5TG")); //nivel 183
        niveles.Add(new Level(13, "QAZWSXEDCRFVTGB")); //nivel 184
        niveles.Add(new Level(13, "1AZ2SX3DC4FV5GB")); //nivel 185
        niveles.Add(new Level(13, "12W34R56Y78I90P")); //nivel 186
        niveles.Add(new Level(13, "PYDSAQW12345678")); //nivel 187
        niveles.Add(new Level(13, "MJUNHYCDEXSW123")); //nivel 188
        niveles.Add(new Level(13, "CXZHGFEWQ654098")); //nivel 189
        niveles.Add(new Level(13, "OIULKJGFDTRE1QA")); //nivel 190
        niveles.Add(new Level(13, "134568QERTYIADF")); //nivel 191
        niveles.Add(new Level(13, "KHGMBVYRE421AZX")); //nivel 192
        niveles.Add(new Level(13, "ASDERT432JHGCXZ")); //nivel 193
        niveles.Add(new Level(13, "0PL9OK8IJQAZEDC")); //nivel 194
        niveles.Add(new Level(13, "QZWXECRVTBYNUJM")); //nivel 195
        niveles.Add(new Level(13, "12W43EASXFDCGHN")); //nivel 196
        niveles.Add(new Level(13, "321EWQDSACXZHGF")); //nivel 197
        niveles.Add(new Level(13, "1WD2EF3RG4TH6UK")); //nivel 198
        niveles.Add(new Level(13, "DSAHGFCXZNBVDE3")); //nivel 199
        niveles.Add(new Level(13, "MBCJGDFUTE753AC")); //nivel 200


    }

    public struct Level
    {
        public int type;
        public string colors;
        /// <summary>
        /// Constructor para el nivel
        /// </summary>
        /// <param name="type">tipo de nivel para asignar sus bordes y sombras correspondientes</param>
        /// <param name="colores">colores para cada circulo, los circulos y colores deben tener la misma cnatidad de caracteres</param>
        public Level(int type, string colores)
        {
            this.type = type;
            this.colors = colores;
        }


    }
}



