<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered = [];
$indexId = 0;

$presence = new stdClass();
$presence->code_etape = false;
$presence->vet = false;
$presence->libelle_vet = false;
$presence->id_comp = false;
$presence->code_diplome = false;
$presence->vdi = false;

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `etape` (";
    $data = "(";

    $strReq .= " `code_etape` ";
    $data .= "'$values->code_etape'";
    array_push($id_entered, $values->code_etape);

    $strReq .= " ,`vet` ";
    $data .= ",'$values->vet'";

    $strReq .= " ,`libelle_vet` ";
    $data .= ",'$values->libelle_vet'";

    $strReq .= " ,`id_comp` ";
    $data .= ",'$values->id_comp'";

    if (isset($values->code_diplome)) {
        $strReq .= " ,`code_diplome` ";
        $data .= ",'$values->code_diplome'";
    }

    if (isset($values->vdi)) {
        $strReq .= " ,`vdi` ";
        $data .= ",'$values->vdi'";
    }

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        $nbRows = $createReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `code_etape`, `vet`, `libelle_vet`, `id_comp`, `code_diplome`, `vdi` FROM `etape` WHERE ";
            $resultStr .= "`code_etape` = '$id_entered[$indexId]'";
            $indexId++;

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_etape = $row->code_etape;
            $obj->vet = $row->vet;
            $obj->libelle_vet = $row->libelle_vet;
            $obj->id_comp = $row->id_comp;
            $obj->code_diplome = $row->code_diplome;
            $obj->vdi = $row->vdi;

            array_push($returnedValues->values, $obj);
            $returnedValues->success = true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    } else {
        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);
