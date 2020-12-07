<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {

    $requete = $db->query("SELECT code_sem,libelle_sem,no_sem,code_etape,vet FROM `semestre`");

    $array = array();

    foreach ($requete as $req) {

        if (array_key_exists('code_sem', $req))
            $code_sem = $req['code_sem'];

        if (array_key_exists('libelle_sem', $req))
            $libelle_sem = $req['libelle_sem'];

        if (array_key_exists('no_sem', $req))
            $no_sem = $req['no_sem'];

        if (array_key_exists('code_etape', $req))
            $code_etape = $req['code_etape'];

        if (array_key_exists('vet', $req))
            $vet = $req['vet'];

        $arrayReq = array(
            'code_sem' => $code_sem,
            'libelle_sem' => $libelle_sem,
            'no_sem' => $no_sem,
            'code_etape' => $code_etape,
            'vet' => $vet
        );
        array_push($array, $arrayReq);
    }

    $semestre["success"] = true;
    $semestre["values"] = $array;
} else {
    echo json_encode($connectionDB);
}

echo json_encode($semestre);
