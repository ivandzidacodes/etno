$(document).ready(function () {
    //Trigger onchange event da se ucitaju cijene za jedinice
   // $("#PonudeJedinice_0__TipStanbeneJedinice").change();

    $("#tblJedinice").on("change", "[id$='StanbenaJedinica']", stanbenaJedinicaPromjenjena);

    $("#myCarousel").on("click", ".pregledSlikeImg", function () {
        $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
        $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
    });
});

function dobaviGradove() {
    var drzavaId = $('#Drzava').val();

    $.ajax({
            url: '/Registracija/DobaviGradove',
            type: 'GET',
            dataType: 'JSON',
            data: { drzavaId: drzavaId },
            success: function (gradovi) {
                $('#Grad').html('');
                $.each(gradovi, function (i, grad) {
                    $("#Grad").append(
                        $('<option></option>').val(grad.Id).html(grad.Naziv));
                });
            }
        });
}

function stanbenaJedinicaPromjenjena() {
    var cijena = $(this).find(":selected").attr('cijena');

    $(this).closest("tr").find("[id*='CijenaBezPopusta']").first().val(cijena);

    izracunajPopust();
}

function ucitajStanbeneJedinice(element) {

    $.ajax({
        url: '/Ponude/DobaviStanbeneJedinice',
        type: 'GET',
        dataType: 'JSON',
        data: { tipStanbeneJedinice: $(element).val() },
        success: function (stanbeneJedinice) {

            var stanbenaJedinicaDdl = $(element).closest('tr').find("[id$='StanbenaJedinica']").first();

            stanbenaJedinicaDdl.html('');

            $.each(stanbeneJedinice, function (i, jedinica) {
                stanbenaJedinicaDdl.append($('<option></option').val(jedinica.Id).attr("cijena", jedinica.Cijena).html(jedinica.Naziv));
            });

            stanbenaJedinicaDdl.change();
        }
    })
}

function dodajJedinicu() {
    $("#Dodaj").val(1);
    dodajPonudu();
}

function dodajAktivnost() {
    $("#Dodaj").val(2);
    dodajPonudu();
}

function dodajPonudu() {
    $('#frmDodajPonudu').attr('action', '/Ponude/DodajPonudu');
    $("#frmDodajPonudu").submit();
}

function izracunajPopust() {
    $("#tblJedinice tr").each(function () {
        var popust = $("#Popust").val();

        var cijena = $(this).find("[id*='CijenaBezPopusta']").first().val();

        var popustProcenat = 1-(popust/100);

        $(this).find("[id*='CijenaSaPopustom']").first().val(cijena * popustProcenat);
    });
}


function izbrisiRed(guid) {
    $("#Guid").val(guid);
    $('#frmDodajPonudu').attr('action', '/Ponude/ObrisiStavkuIzPonude');
    $("#frmDodajPonudu").submit();
}

function pregledPonude() {
    $('#frmDodajPonudu').attr('action', '/Ponude/PregledPonude');
    $("#frmDodajPonudu").submit();
}

function eksportujUPdf(val) {
    $("#izvjestaj").val(val);
    $("#izvjestaj").closest("form").submit();
}

function dodajSlike() {
    $('#slike').click();
}

function ucitajImenaSlika() {
    console.log('hello from ucitaj imena slika');
    var list = $("#imenaSlika");
    list.html('');

    var slike = $("#slike")[0];

    $(slike.files).each(function (index) {
        list.append('<li>' + this.name + '</li>');
        console.log('each');
    });
}