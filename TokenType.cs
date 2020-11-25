﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slan
{
    enum TokenType
    {
        LEFT_PAREN , RIGHT_PAREN , LEFT_BRACE , RIGHT_BRACE,
        COMMA , DOT , MINUS , PLUS , SEMICOLON , SLASH , STAR,

        BANG , BANG_EQUAL,
        EQUAL , EQUAL_EQUAL,
        GREATER , GREATER_EQUAL,
        LESS , LESS_EQUAL,


        IDENTIFIER , STRING , NUMBER,


        AND, CLASS , ELSE , FALSE , FUN , IF , NULL, OR,
        PRINT , RETURN , SUPER , THIS , TRUE , VAR , WHILE,

        EOF
    }
}
