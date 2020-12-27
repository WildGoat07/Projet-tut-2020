<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $firstValue = true;

    $strReq = "UPDATE `diplome` SET ";

    $data = $values->data;

    if (isset($data->code_diplome)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_diplome` = '$data->code_diplome'";
    }
    if (isset($data->libelle_diplome)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`libelle_diplome` = '$data->libelle_diplome'";
    }
    if (isset($data->vdi)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`vdi` = '$data->vdi'";
    }
    if (isset($data->libelle_vdi)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`libelle_vdi` = '$data->libelle_vdi'";
    }
    if (isset($data->annee_deb)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`annee_deb` = '$data->annee_deb'";
    }
    if (isset($data->annee_fin)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`annee_fin` = '$data->annee_fin'";
    }

    $target = $values->target;
    $strReq .= " WHERE `code_diplome` = '$target->code_diplome' AND `vdi` = '$target->vdi' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `code_diplome`, `libelle_diplome`, `vdi`, `libelle_vdi`, `annee_deb`, `annee_fin` FROM `diplome` WHERE ";
            if (isset($data->code_diplome))
                $resultStr .= "`code_diplome`='$data->code_diplome'";
            else
                $resultStr .= "`code_diplome`='$target->code_diplome'";

            if (isset($data->vdi))
                $resultStr .= " AND `vdi`='$data->vdi'";
            else
                $resultStr .= " AND `vdi`='$target->vdi'";

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
        } 
        else {
            $returnedValues->success=false;
        
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $returnedValues->errors[] = $obj;
        }
    } 
    else {
        $returnedValues->success=false;

        $obj = new stdClass();
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);