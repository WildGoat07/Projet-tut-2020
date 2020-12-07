<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {

    $requete = $db->query("SELECT * FROM `diplome`");

    $array = array();

    foreach ($requete as $req) {

        if (array_key_exists('code_diplome', $req))
            $code_diplome = $req['code_diplome'];

        if (array_key_exists('libelle_diplome', $req))
            $libelle_diplome = $req['libelle_diplome'];

        if (array_key_exists('vdi', $req))
            $vdi = $req['vdi'];

        if (array_key_exists('libelle_vdi', $req))
            $libelle_vdi = $req['libelle_vdi'];

        if (array_key_exists('annee_deb', $req))
            $annee_deb = $req['annee_deb'];

        if (array_key_exists('annee_fin', $req))
            $annee_fin = $req['annee_fin'];

        $arrayReq = array(
            'code_diplome' => $code_diplome,
            'libelle_diplome' => $libelle_diplome,
            'vdi' => $vdi,
            'libelle_vdi' => $libelle_vdi,
            'annee_deb' => $annee_deb,
            'annee_fin' => $annee_fin
        );
        array_push($array, $arrayReq);
    }

    $diplome["success"] = true;
    $diplome["results"]["Diplomes"] = $array;
} else {
    echo json_encode($connectionDB);
}


echo json_encode($diplome);
