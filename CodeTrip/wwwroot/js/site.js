function confirmarExclusao(id) {
    Swal.fire({
        title: 'Confirmar Exclusão?',
        text: "Essa ação não pode ser desfeita! ⚠️",
        showCancelButton: true,
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: 'LightSeaGreen',
        cancelButtonColor: 'Grey'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                icon: 'success',
                title: 'Sucesso!',
                text: "A exclusao foi realizada com sucesso.",
                timer: 2000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(() => {
                window.location.href = '/Cliente/ExcluirCliente/' + id
            })
        }
    });
}

function confirmarExclusaoHospedagem(id) {
    Swal.fire({
        title: 'Confirmar Exclusão?',
        text: "Essa ação não pode ser desfeita! ⚠️",
        showCancelButton: true,
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: 'LightSeaGreen',
        cancelButtonColor: 'Grey'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                icon: 'success',
                title: 'Sucesso!',
                text: "A exclusao foi realizada com sucesso.",
                timer: 2000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(() => {
                window.location.href = '/Hospedagem/ExcluirHospedagem/' + id
            })
        }
    });
}

function confirmarExclusaoTransporte(id) {
    Swal.fire({
        title: 'Confirmar Exclusão?',
        text: "Essa ação não pode ser desfeita! ⚠️",
        showCancelButton: true,
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: 'LightSeaGreen',
        cancelButtonColor: 'Grey'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                icon: 'success',
                title: 'Sucesso!',
                text: "A exclusao foi realizada com sucesso.",
                timer: 2000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(() => {
                window.location.href = '/Transporte/ExcluirTransporte/' + id
            })
        }
    });
}

function confirmarExclusaoPedido(id) {
    Swal.fire({
        title: 'Confirmar Exclusão?',
        text: "Essa ação não pode ser desfeita! ⚠️",
        showCancelButton: true,
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar',
        confirmButtonColor: 'LightSeaGreen',
        cancelButtonColor: 'Grey'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                icon: 'success',
                title: 'Sucesso!',
                text: "A exclusao foi realizada com sucesso.",
                timer: 2000,
                showCancelButton: false,
                showConfirmButton: false
            }).then(() => {
                window.location.href = '/Pedido/ExcluirPedido/' + id
            })
        }
    });
}
