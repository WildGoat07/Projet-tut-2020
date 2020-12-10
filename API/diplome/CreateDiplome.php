<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered = [];
$indexId = 0;

$presence = new stdClass();
$presence->code_diplome = false;
$presence->libelle_diplome = false;
$presence->vdi = false;
$presence->libelle_vdi = false;
$presence->annee_deb = false;
$presence->annee_fin = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `diplome` (";
    $data = "(";

    $strReq .= " `code_diplome` ";
    $data .= "'$values->code_diplome'";
    array_push($id_entered, $values->code_diplome);

    $strReq .= " ,`libelle_diplome` ";
    $data .= ",'$values->libelle_diplome'";

    $strReq .= " ,`vdi` ";
    $data .= ",'$values->vdi'";

    $strReq .= " ,`libelle_vdi` ";
    $data .= ",'$values->libelle_vdi'";

    if (isset($values->annee_deb)) {
        $strReq .= " ,`annee_deb` ";
        $data .= ",'$values->annee_deb'";
    }

    if (isset($values->annee_fin)) {
        $strReq .= " ,`annee_fin` ";
        $data .= ",'$values->annee_fin'";
    }

    $strReq .= ") VALUES $data )";

    $requete = $db->prepare($strReq);

    if ($requete->execute()) {
        $resultStr = "SELECT `code_diplome`, `libelle_diplome`, `vdi`, `libelle_vdi`, `annee_deb`, `annee_fin` FROM `diplome` WHERE ";
        $resultStr .= "`code_diplome` = '$id_entered[$indexId]'";
        $indexId++;

        $result = $db->query($resultStr);
        $row = $result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->code_diplome = $row->code_diplome;
        $obj->libelle_diplome = $row->libelle_diplome;
        $obj->vdi = $row->vdi;
        $obj->libelle_vdi = $row->libelle_vdi;
        $obj->annee_deb = $row->annee_deb;
        $obj->annee_fin = $row->annee_fin;

        array_push($returnedValues->values, $obj);
        $returnedValues->success = true;
    } else {
        $error = $requete->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);
