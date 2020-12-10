<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $strReq = "SELECT `id_comp` FROM `comp_courante`";
    $postObj = json_decode(file_get_contents('php://input'));
    if (isset($postObj->filters)) {
        $firstFilter = true;
        $whereSet = false;
        if (isset($postObj->filters->id_comp)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->id_comp as $id_comp) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`id_comp` = \"$id_comp\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
    }

    if (isset($postObj->order))
        if (isset($postObj->reverse_order) && $postObj->reverse_order)
            $strReq .= " ORDER BY DESC `$postObj->order`";
        else
            $strReq .= " ORDER BY `$postObj->order`";
    else if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY DESC `id_comp`";
    else
        $strReq .= " ORDER BY `id_comp`";
    $strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

    $requete = $db->query($strReq);

    $comp_courante = new stdClass();
    $comp_courante->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->id_comp = utf8_encode($req['id_comp']);

        $comp_courante->values[] = $obj;
    }

    $comp_courante->success = true;

    echo json_encode($comp_courante);
} else {
    echo json_encode($connectionDB);
}
