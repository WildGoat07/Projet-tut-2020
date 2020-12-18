<?php

function cleanString($text) {
    $utf8 = array(
        '/[áàâãªä]/u'   =>   'a',
        '/[ÁÀÂÃÄ]/u'    =>   'a',
        '/[ÍÌÎÏ]/u'     =>   'i',
        '/[íìîï]/u'     =>   'i',
        '/[éèêë]/u'     =>   'e',
        '/[ÉÈÊË]/u'     =>   'e',
        '/[óòôõºö]/u'   =>   'o',
        '/[ÓÒÔÕÖ]/u'    =>   'o',
        '/[úùûü]/u'     =>   'u',
        '/[ÚÙÛÜ]/u'     =>   'u',
        '/ç/'           =>   'c',
        '/Ç/'           =>   'c',
        '/ñ/'           =>   'n',
        '/Ñ/'           =>   'n',
        '/ /'           =>   '', // nonbreaking space (equiv. to 0x160)
        '/ /'           =>   '',
    );

    $temp='';
    foreach(  str_split(strtolower(preg_replace(array_keys($utf8), array_values($utf8), $text))) as $char) {
        if (preg_match('/[a-z]/', $char ) || preg_match('/\d/', $char))
            $temp .= $char;
    }

    return $temp;
}