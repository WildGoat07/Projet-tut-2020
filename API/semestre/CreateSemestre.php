<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered = [];
$indexId = 0;

$presence = new stdClass();
$presence->code_sem = false;
$presence->libelle_sem = false;
$presence->no_sem = false;
$presence->code_etape = false;
$presence->vet = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `semestre` (";
    $data = "(";

    $strReq .= " `code_sem` ";
    $data .= "'$values->code_sem'";
    array_push($id_entered, $values->code_sem);

    $strReq .= " ,`libelle_sem` ";
    $data .= ",'$values->libelle_sem'";

    if (isset($values->no_sem)) {
        $strReq .= " ,`no_sem` ";
        $data .= ",'$values->no_sem'";
    }

    if (isset($values->code_etape)) {
        $strReq .= " ,`code_etape` ";
        $data .= ",'$values->code_etape'";
    }

    if (isset($values->vet)) {
        $strReq .= " ,`vet` ";
        $data .= ",'$values->vet'";
    }

    $strReq .= ") VALUES $data )";

    $requete = $db->prepare($strReq);

    if ($requete->execute()) {
        $resultStr = "SELECT `code_sem`, `libelle_sem`, `no_sem`, `code_etape`, `vet` FROM `semestre` WHERE ";
        $resultStr .= "code_sem = $id_entered[$indexId]";
        $indexId++;

        $result = $db->query($resultStr);
        $row = $result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->code_sem = $row->code_sem;
        $obj->libelle_sem = $row->libelle_sem;
        $obj->no_sem = $row->no_sem;
        $obj->code_etape = $row->code_etape;
        $obj->vet = $row->vet;

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
