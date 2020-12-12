<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `composante` SET ";

    $data = $values->data;

    if (isset($data->id_comp))
        $strReq .= "`id_comp` = '$data->id_comp'";
    if (isset($data->nom_comp))
        $strReq .= ",`nom_comp` = '$data->nom_comp'";
    if (isset($data->lieu_comp))
        $strReq .= ",`lieu_comp` = '$data->lieu_comp'";

    $target = $values->target;
    $strReq .= " WHERE `id_comp` = '$target->id_comp' ";

    $updateReq = $db->prepare($strReq);
    if ($updateReq->execute()) {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `id_comp`, `nom_comp`, `lieu_comp` FROM `composante` WHERE ";
            $resultStr .= "`id_comp` = '$data->id_comp'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_comp = $row->id_comp;
            $obj->nom_comp = $row->nom_comp;
            $obj->lieu_comp = $row->lieu_comp;

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
