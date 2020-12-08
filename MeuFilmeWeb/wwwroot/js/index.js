var templateIndex =
{
    PesquisarFilme: function (texto) {
        return templateIndex.EfetuarPesquisa(texto);
    },
    EfetuarPesquisa: function (texto) {

        var resultado = '';
        var linhasFilmes = [];
        var result = $.ajax({
            url: "https://meufilmegateway20201204104342.azurewebsites.net/filme/p/" + texto,
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

        for (var i = 0; i < result.responseJSON["collection"].length; i++) {
            linhasFilmes.push(result.responseJSON["collection"][i].Value);
        }
        resultado = templateIndex.ConfigurarTabela(linhasFilmes);


        return resultado;
    },
    ConfigurarTabela: function (linhasFilmes) {
        var resultado = '';
        moment.locale('pt-br');
        for (var i = 0; i < linhasFilmes.length; i++) {
            resultado += '<div class="col-sm mt-sm-3">';
            resultado += '<div class="card" style="width: 18rem;">';
            if (linhasFilmes[i].poster != null && linhasFilmes[i].poster != undefined && linhasFilmes[i].poster != '') {
                resultado += '    <img class="card-img-top" src="https://image.tmdb.org/t/p/original/' + linhasFilmes[i].poster + '" alt="' + linhasFilmes[i].nome + '">';
            }
            resultado += '        <div class="card-body">';
            resultado += '            <h5 class="card-title">' + linhasFilmes[i].nome + ' (' + moment(linhasFilmes[i].dataLancamento).format('YYYY') + ')</h5>';
            resultado += '            <small>' + linhasFilmes[i].nomeOriginal + ' (' + linhasFilmes[i].votosMedia + ')</small>';
            resultado += '            <p class="card-text">' + linhasFilmes[i].resumo + '</p>';
            resultado += '            <a href="Filme/' + linhasFilmes[i].id + '" class="btn btn-primary">Mais</a>';
            resultado += '        </div>';
            resultado += '</div>';
            resultado += '</div>';
        }

        return resultado;
    }
}