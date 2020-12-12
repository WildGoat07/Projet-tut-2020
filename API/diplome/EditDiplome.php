<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `diplome` SET ";

    $data = $values->data;

    if (isset($data->code_diplome))
        $strReq .= "`code_diplome` = '$data->code_diplome'";
    if (isset($data->libelle_diplome))
        $strReq .= ",`libelle_diplome` = '$data->libelle_diplome'";
    if (isset($data->vdi))
        $strReq .= ",`vdi` = '$data->vdi'";
    if (isset($data->libelle_vdi))
        $strReq .= ",`libelle_vdi` = '$data->libelle_vdi'";
    if (isset($data->annee_deb))
        $strReq .= ",`annee_deb` = '$data->annee_deb'";
    if (isset($data->annee_fin))
        $strReq .= ",`annee_fin` = '$data->annee_fin'";

    $target = $values->target;
    $strReq .= " WHERE `code_diplome` = '$target->code_diplome' ";

    $updateReq = $db->prepare($strReq);
    if ($updateReq->execute()) {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `code_diplome`, `libelle_diplome`, `vdi`, `libelle_vdi`, `annee_deb`, `annee_fin` FROM `diplome` WHERE ";
            $resultStr .= "`code_diplome` = '$data->code_diplome'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_diplome = $row->code_diplome;
            $obj->libelle_diplome = $row->libelle_diplome;
            $obj->vdi = $row->vdi;
            $obj->libelle_vdi = $row->libelle_vdi;
            $obj->annee_deb = $row->annee_deb;
            $obj->annee_fin = $row->annee_fin;

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
