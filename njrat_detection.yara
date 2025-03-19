rule Detect_NjRat_And_Payload {
    meta:
        author = "Analyse_Malware"
        description = "Détecte NjRat et les exécutables qu'il génère"
        date = "2025-03-11"

    strings:
        // Signature de NjRat
        $njrat_sig = "NjRat" wide ascii
        $njrat_config = "NjRat 0.7D" wide ascii
        $njrat_reg = "Software\\Microsoft\\Windows\\CurrentVersion\\Run" wide ascii

        // Indicateurs des exécutables générés par NjRat
        $payload_exec1 = "client.exe" wide ascii
        $payload_exec2 = "server.exe" wide ascii
        $payload_exec3 = "stub.exe" wide ascii

        // API courantes utilisées pour la persistance et l'exécution
        $api_createproc = "CreateProcessA" wide ascii
        $api_registry = "RegSetValueExA" wide ascii
        $api_tcp = "connect" wide ascii

    condition:
        (any of ($njrat_sig, $njrat_config, $njrat_reg)) or
        (any of ($payload_exec1, $payload_exec2, $payload_exec3)) or
        (all of ($api_createproc, $api_registry, $api_tcp))
}
