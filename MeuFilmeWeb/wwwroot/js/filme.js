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
        resultado += '<div class="row">';
        resultado += '    <div class="col-md-3">';
        if (filmeJSON.poster != null && filmeJSON.poster != undefined && filmeJSON.poster != '') {
            resultado += '    <img src="https://image.tmdb.org/t/p/w185/' + filmeJSON.poster + '" alt="' + filmeJSON.nome + '" >';
        }
        resultado += '    </div>';
        resultado += '    <div class="col-md-7">';
        resultado += '        <h1>' + filmeJSON.nome + ' (' + moment(filmeJSON.dataLancamento).format('YYYY') + ')</h1>';
        resultado += '        <p class="title">' + filmeJSON.nomeOriginal + ' (' + filmeJSON.votosMedia + ')</p>';
        resultado += '        <p>';
        for (var i = 0; i < filmeJSON.genero.length; i++) {
            genero += ' ' + filmeJSON.genero[i].nome + ' |';
        }
        resultado += templateFilme.trimChar(genero, '|');
        resultado += '        </p>';
        resultado += '        <p>' + filmeJSON.resumo + '</p>';
        resultado += '    </div>';
        resultado += '</div>';
        resultado += '<div class="row"><div class="col-md-12">&nbsp;</div></div>';
        resultado += '<div class="row">';
        resultado += '    <div class="col-md-6">';
        resultado += '        <p>Elenco</p>';
        resultado += '        <div class="list-group">';
        for (var i = 0; i < filmeJSON.elenco.length; i++) {
            resultado += '        <a  class="list-group-item list-group-item-action flex-column align-items-start"> <div class="row"><div class="col-8"><div class="row p-3"><div class="col-12"><h5 class="mb-1">' + filmeJSON.elenco[i].nome + '</h5></div><div class="col-12">' + filmeJSON.elenco[i].personagem + '</div>';
            resultado += '</div></div>';
            if (filmeJSON.elenco[i].foto != null && filmeJSON.elenco[i].foto != undefined && filmeJSON.elenco[i].foto != '') {
                resultado += '<div class="col-3"><img src="https://image.tmdb.org/t/p/w92/' + filmeJSON.elenco[i].foto + '" class="d-block float-right img-thumbnail img-fluid"></div>';
            }
            resultado += '</div></a>';
        }
        resultado += '        </div>';
        resultado += '    </div>';
        resultado += '    <div class="col-md-6">';
        resultado += '        <p>Equipe</p>';
        resultado += '        <div class="list-group">';
        for (var i = 0; i < filmeJSON.equipe.length; i++) {
            resultado += '        <a class="list-group-item list-group-item-action flex-column align-items-start"> <div class="row"><div class="col-8"><div class="row p-3"><div class="col-12"><h5 class="mb-1">' + filmeJSON.equipe[i].nome + '</h5></div><div class="col-12">' + filmeJSON.equipe[i].cargo + '</div>';
            resultado += '</div></div>';
            if (filmeJSON.equipe[i].foto != null && filmeJSON.equipe[i].foto != undefined && filmeJSON.equipe[i].foto != '') {
                resultado += '<div class="col-3"><img src="https://image.tmdb.org/t/p/w92/' + filmeJSON.equipe[i].foto + '" class="d-block float-right img-thumbnail img-fluid"></div>';
            }
            resultado += '</div></a>';
        }
        resultado += '        </div>';
        resultado += '    </div>';
        resultado += '</div>';
        resultado += '<div class="row"><div class="col-md-12"><a type="button" class="btn btn-secondary d-flex justify-content-center" href="/' + filmeJSON.nome + '">Voltar</a></div></div>';
        resultado += '</div>';



        //if (filmeJSON.poster != null && filmeJSON.poster != undefined && filmeJSON.poster != '') {
        //    resultado += '    <img src="https://image.tmdb.org/t/p/original/' + filmeJSON.poster + '" alt="' + filmeJSON.nome + '" style="width:100%">';
        //}
        //resultado += '        <h1>' + filmeJSON.nome + ' (' + moment(filmeJSON.dataLancamento).format('YYYY') + ')</h1>';
        //resultado += '        <p class="title">' + filmeJSON.nomeOriginal + ' (' + filmeJSON.votosMedia + ')</p>';
        //resultado += '        <p>';
        //for (var i = 0; i < filmeJSON.genero.length; i++) {
        //    genero += ' '+filmeJSON.genero[i].nome + ' |';
        //}
        //resultado += templateFilme.trimChar(genero,'|');
        //resultado += '        </p>';
        //resultado += '        <p>' + filmeJSON.resumo + '</p>';
        //resultado += '        <p>Elenco</p>';
        //resultado += '        <p>';
        //for (var i = 0; i < filmeJSON.elenco.length; i++) {
        //    resultado += '        <p>' + filmeJSON.elenco[i].nome + ' - ' + filmeJSON.elenco[i].personagem + '</p>';
        //}
        //resultado += '        </p>';
        //resultado += '        <p>Equipe</p>';
        //resultado += '        <p>';
        //for (var i = 0; i < filmeJSON.equipe.length; i++) {
        //    resultado += '        <p>' + filmeJSON.equipe[i].nome + ' - ' + filmeJSON.equipe[i].cargo + '</p>';
        //}
        //resultado += '        </p>';
        //resultado += '        <a type="button" class="btn btn-secondary" href="/' + filmeJSON.nome + '">Voltar</a>';
        //resultado += '</div>';

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