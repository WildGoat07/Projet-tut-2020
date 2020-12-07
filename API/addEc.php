<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    if (!empty($_POST["code_ec"]) && !empty($_POST["libelle_ec"])) {

        $code_ec = htmlspecialchars($_POST['code_ec']);
        $libelle_ec = htmlspecialchars($_POST['libelle_ec']);
        if (!empty($_POST["nature"])) {
            $nature = htmlspecialchars($_POST['nature']);
        } else {
            $nature = "E";
        }
        if (!empty($_POST["HCM"])) {
            $HCM = htmlspecialchars($_POST['HCM']);
        } else {
            $HCM = null;
        }
        if (!empty($_POST["HEI"])) {
            $HEI = htmlspecialchars($_POST['HEI']);
        } else {
            $HEI = null;
        }
        if (!empty($_POST["HTD"])) {
            $HTD = htmlspecialchars($_POST['HTD']);
        } else {
            $HTD = null;
        }
        if (!empty($_POST["HTP"])) {
            $HTP = htmlspecialchars($_POST['HTP']);
        } else {
            $HTP = null;
        }
        if (!empty($_POST["HTPL"])) {
            $HTPL = htmlspecialchars($_POST['HTPL']);
        } else {
            $HTPL = null;
        }
        if (!empty($_POST["HPRJ"])) {
            $HPRJ = htmlspecialchars($_POST['HPRJ']);
        } else {
            $HPRJ = null;
        }
        if (!empty($_POST["NbEpr"])) {
            $NbEpr = htmlspecialchars($_POST['NbEpr']);
        } else {
            $NbEpr = 1;
        }
        if (!empty($_POST["CNU"])) {
            $CNU = htmlspecialchars($_POST['CNU']);
        } else {
            $CNU = 2700;
        }
        if (!empty($_POST["no_cat"])) {
            $no_cat = htmlspecialchars($_POST['no_cat']);
        } else {
            $no_cat = null;
        }
        if (!empty($_POST["code_ec_pere"])) {
            $code_ec_pere = htmlspecialchars($_POST['code_ec_pere']);
        } else {
            $code_ec_pere = null;
        }
        if (!empty($_POST["code_ue"])) {
            $code_ue = htmlspecialchars($_POST['code_ue']);
        } else {
            $code_ue = null;
        }
        $requete = $db->query("INSERT INTO ec SET code_ec = ?, libelle_ec = ?, nature = ?, HCM = ?, HEI = ?, HTD = ?, HTP = ?, HTPL= ?, HPRJ = ?, NbEpr = ?, CNU = ?, no_cat = ?, code_ec_pere = ?, code_ue = ?", [
            $code_ec, $libelle_ec, $nature, $HCM,
            $HEI, $HTD, $HTP, $HTPL, $HPRJ, $NbEpr, $CNU, $no_cat, $code_ec_pere, $code_ue
        ]);


        $ec["success"] = true;
        $ec["message"] = "L'ec a bien été ajouté";
    } else {
        $ec["success"] = false;
        $ec["message"] = "Il manque des informations !";
    }
} else {
    echo json_encode($connectionDB);
}

echo json_encode($ec);
