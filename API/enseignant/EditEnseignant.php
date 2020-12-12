<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `enseignant` SET ";

    $data = $values->data;

    if (isset($data->id_ens))
        $strReq .= "`id_ens` = '$data->id_ens'";
    if (isset($data->nom))
        $strReq .= ",`nom` = '$data->nom'";
    if (isset($data->prenom))
        $strReq .= ",`prenom` = '$data->prenom'";
    if (isset($data->fonction))
        $strReq .= ",`fonction` = '$data->fonction'";
    if (isset($data->HOblig))
        $strReq .= ",`HOblig` = '$data->HOblig'";
    if (isset($data->HMax))
        $strReq .= ",`HMax` = '$data->HMax'";
    if (isset($data->CRCT))
        $strReq .= ",`CRCT` = '$data->CRCT'";
    if (isset($data->PES_PEDR))
        $strReq .= ",`PES_PEDR` = '$data->PES_PEDR'";
    if (isset($data->id_comp))
        $strReq .= ",`id_comp` = '$data->id_comp'";

    $target = $values->target;
    $strReq .= " WHERE `id_ens` = '$target->id_ens' ";

    $updateReq = $db->prepare($strReq);
    if ($updateReq->execute()) {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp` FROM `enseignant` WHERE ";
            $resultStr .= "`id_ens` = '$data->id_ens'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_ens = $row->id_ens;
            $obj->nom = $row->nom;
            $obj->prenom = $row->prenom;
            $obj->fonction = $row->fonction;
            $obj->HOblig = $row->HOblig;
            $obj->HMax = $row->HMax;
            $obj->CRCT = $row->CRCT;
            $obj->PES_PEDR = $row->PES_PEDR;
            $obj->id_comp = $row->id_comp;

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
