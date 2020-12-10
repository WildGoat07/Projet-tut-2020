<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

    if (!empty($_POST["id_ens"]) && !empty($_POST["code_ec"]) && !empty($_POST["annee"])) {


        $id_ens = htmlspecialchars($_POST['id_ens']);
        $code_ec = htmlspecialchars($_POST['code_ec']);
        $annee = htmlspecialchars($_POST['annee']);
        if (!empty($_POST["NbGpCM"])) {
            $NbGpCM = htmlspecialchars($_POST['NbGpCM']);
        } else {
            $NbGpCM = null;
        }
        if (!empty($_POST["NbGpEI"])) {
            $NbGpEI = htmlspecialchars($_POST['NbGpEI']);
        } else {
            $NbGpEI = null;
        }
        if (!empty($_POST["NBGpTD"])) {
            $NBGpTD = htmlspecialchars($_POST['NBGpTD']);
        } else {
            $NBGpTD = null;
        }
        if (!empty($_POST["NbGpTP"])) {
            $NbGpTP = htmlspecialchars($_POST['NbGpTP']);
        } else {
            $NbGpTP = null;
        }
        if (!empty($_POST["NbGpTPL"])) {
            $NbGpTPL = htmlspecialchars($_POST['NbGpTPL']);
        } else {
            $NbGpTPL = null;
        }
        if (!empty($_POST["NBGpPRJ"])) {
            $NBGpPRJ = htmlspecialchars($_POST['NBGpPRJ']);
        } else {
            $NBGpPRJ = null;
        }
        if (!empty($_POST["HEqTD"])) {
            $HEqTD = htmlspecialchars($_POST['HEqTD']);
        } else {
            $HEqTD = null;
        }


        $requete = $db->query("INSERT INTO service SET id_ens = ?, code_ec = ?, annee = ?, NbGpCM = ?, NbGpEI = ?, NBGpTD = ?, NbGpTP = ?, NbGpTPL = ?, NBGpPRJ = ?, HEqTD = ?", [
            $id_ens, $code_ec,
            $annee, $NbGpCM, $NbGpEI, $NBGpTD, $NbGpTP, $NbGpTPL, $NBGpPRJ, $HEqTD
        ]);

        $service["success"] = true;
        $service["message"] = "Le service a bien été ajouté";
    } else {
        $service["success"] = false;
        $service["message"] = "Il manque des informations !";
    }

echo json_encode($service);
