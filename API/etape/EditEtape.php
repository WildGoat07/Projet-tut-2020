<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `etape` SET ";

    $data = $values->data;

    if (isset($data->code_etape))
        $strReq .= "`code_etape` = '$data->code_etape'";
    if (isset($data->vet))
        $strReq .= ",`vet` = '$data->vet'";
    if (isset($data->libelle_vet))
        $strReq .= ",`libelle_vet` = '$data->libelle_vet'";
    if (isset($data->id_comp))
        $strReq .= ",`id_comp` = '$data->id_comp'";
    if (isset($data->code_diplome))
        $strReq .= ",`code_diplome` = '$data->code_diplome'";
    if (isset($data->vdi))
        $strReq .= ",`vdi` = '$data->vdi'";

    $target = $values->target;
    $strReq .= " WHERE `code_etape` = '$target->code_etape' ";

    $updateReq = $db->prepare($strReq);
    if ($updateReq->execute()) {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `code_etape`, `vet`, `libelle_vet`, `id_comp`, `code_diplome`, `vdi` FROM `etape` WHERE ";
            $resultStr .= "`code_etape` = '$data->code_etape'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_etape = $row->code_etape;
            $obj->vet = $row->vet;
            $obj->libelle_vet = $row->libelle_vet;
            $obj->id_comp = $row->id_comp;
            $obj->code_diplome = $row->code_diplome;
            $obj->vdi = $row->vdi;

            $returnedValues->values[] = $obj;
            $returnedValues->success = true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    } else {
        $error = $updateReq->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);
