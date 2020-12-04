var templateFilme =
{
    ObterFilme: function (filmeId) {
        $("#MeuFilme").html(templateFilme.EfetuarObtencao(filmeId));
    },
    EfetuarObtencao: function (filmeId) {

        var resultado = '';
        var linhasFilmes = [];
        var result = $.ajax({
            url: "https://meufilmegateway20201204104342.azurewebsites.net/filme/o/" + filmeId,
            type: "GET",
            contentType: "application/json",
            dataType: 'json',
            crossDomain: true,
            async: false,
            "headers": {
                "Access-Control-Allow-Headers": "Content-Type, Authorization, Accept, X-Requested-With,X-Foo",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, OPTIONS",
                "X-Foo": "header to trigger preflight"
            }
        })
            .done(function (msg) {
                for (var filme in msg.value) {
                    linhasFilmes.push(filme);
                }
            })
            .fail(function (jqXHR, textStatus, msg) {
                linhasFilmes = [];
            });

        if (result.responseJSON != null) {
            resultado = templateFilme.ConfigurarTabela(result.responseJSON.Value);
        }


        return resultado;
    },
    ConfigurarTabela: function (filmeJSON) {
        var resultado = '';
        var genero = '';
        moment.locale('pt-br');

        resultado += '<div class="card" style="width: 50rem;">';
        if (filmeJSON.poster != null && filmeJSON.poster != undefined && filmeJSON.poster != '') {
            resultado += '    <img src="https://image.tmdb.org/t/p/original/' + filmeJSON.poster + '" alt="' + filmeJSON.nome + '" style="width:100%">';
        }
        resultado += '        <h1>' + filmeJSON.nome + ' (' + moment(filmeJSON.dataLancamento).format('YYYY') + ')</h1>';
        resultado += '        <p class="title">' + filmeJSON.nomeOriginal + ' (' + filmeJSON.votosMedia + ')</p>';
        resultado += '        <p>';
        for (var i = 0; i < filmeJSON.genero.length; i++) {
            genero += ' '+filmeJSON.genero[i].nome + ' |';
        }
        resultado += templateFilme.trimChar(genero,'|');
        resultado += '        </p>';
        resultado += '        <p>' + filmeJSON.resumo + '</p>';
        resultado += '        <p>Elenco</p>';
        resultado += '        <p>';
        for (var i = 0; i < filmeJSON.elenco.length; i++) {
            resultado += '        <p>' + filmeJSON.elenco[i].nome + ' - ' + filmeJSON.elenco[i].personagem + '</p>';
        }
        resultado += '        <p>';
        resultado += '        <p>Equipe</p>';
        resultado += '        <p>';
        for (var i = 0; i < filmeJSON.equipe.length; i++) {
            resultado += '        <p>' + filmeJSON.equipe[i].nome + ' - ' + filmeJSON.equipe[i].cargo + '</p>';
        }
        resultado += '        <p>';
        resultado += '        <a type="button" class="btn btn-secondary" href="/' + filmeJSON.nome + '">Voltar</a>';
        resultado += '</div>';

        return resultado;
    }, trimChar: function (string, charToRemove) {
        while (string.charAt(0) == charToRemove) {
            string = string.substring(1);
        }

        while (string.charAt(string.length - 1) == charToRemove) {
            string = string.substring(0, string.length - 1);
        }

        return string;
    }
}