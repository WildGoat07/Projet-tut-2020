<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $requete = $db->query("SELECT * FROM ec")->fetchAll();

    $array = array();

    foreach ($requete as $req) {

        if (array_key_exists('code_ec', $req))
            $code_ec = $req['code_ec'];

        if (array_key_exists('libelle_ec', $req))
            $libelle_ec = $req['libelle_ec'];

        if (array_key_exists('nature', $req))
            $nature = $req['nature'];

        if (array_key_exists('HCM', $req))
            $HCM = $req['HCM'];

        if (array_key_exists('HEI', $req))
            $HEI = $req['HEI'];

        if (array_key_exists('HTD', $req))
            $HTD = $req['HTD'];

        if (array_key_exists('HTP', $req))
            $HTP = $req['HTP'];

        if (array_key_exists('HTPL', $req))
            $HTPL = $req['HTPL'];

        if (array_key_exists('HPRJ', $req))
            $HPRJ = $req['HPRJ'];

        if (array_key_exists('NbEpr', $req))
            $NbEpr = $req['NbEpr'];

        if (array_key_exists('CNU', $req))
            $CNU = $req['CNU'];

        if (array_key_exists('no_cat', $req))
            $no_cat = $req['no_cat'];

        if (array_key_exists('code_ec_pere', $req))
            $code_ec_pere = $req['code_ec_pere'];

        if (array_key_exists('code_ue', $req))
            $code_ue = $req['code_ue'];

        $arrayReq = array(
            'code_ec' => $code_ec,
            'libelle_ec' => $libelle_ec,
            'nature' => $nature,
            'HCM' => $HCM,
            'HEI' => $HEI,
            'HTD' => $HTD,
            'HTP' => $HTP,
            'HTPL' => $HTPL,
            'HPRJ' => $HPRJ,
            'NbEpr' => $NbEpr,
            'CNU' => $CNU,
            'no_cat' => $no_cat,
            'code_ec_pere' => $code_ec_pere,
            'code_ue' => $code_ue
        );
        array_push($array, $arrayReq);
    }

    $ec["success"] = true;
    $ec["results"]["nb"] = count($array);
    $ec["results"]["Ec"] = $array;
} else {
    echo json_encode($connectionDB);
}

echo json_encode($ec);
