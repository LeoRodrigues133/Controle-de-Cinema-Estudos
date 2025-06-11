document.addEventListener("DOMContentLoaded", function () {
    const maisMeia = document.getElementById('maisMeia');
    const menosMeia = document.getElementById('menosMeia');
    const maisInteira = document.getElementById('maisInteira');
    const menosInteira = document.getElementById('menosInteira');
    const checkboxes = document.querySelectorAll('.assento-checkbox');
    const meiasInput = document.getElementById('meiasInput');
    const inteirasInput = document.getElementById('inteirasInput');
    const erroDistribuicao = document.getElementById('erroDistribuicao');
    const valorTotal = document.getElementById('valorTotal');
    const dadosCarrinho = document.getElementById('dadosCarrinho');
    const btnFinalizar = document.getElementById('btnFinalizar');

    function atualizarCarrinho() {
        const assentosSelecionados = Array.from(checkboxes).filter(cb => cb.checked).map(cb => cb.value);
        const total = assentosSelecionados.length;

        let meias = parseInt(meiasInput.value) || 0;
        let inteiras = parseInt(inteirasInput.value) || 0;

        meiasInput.max = total - inteiras;
        inteirasInput.max = total - meias;

        if (meias > total - inteiras) meias = meiasInput.value = total - inteiras;
        if (inteiras > total - meias) inteiras = inteirasInput.value = total - meias;

        const soma = meias + inteiras;
        const restantes = total - soma;
        document.getElementById('restantes').innerText = restantes;

        if (soma > total) {
            erroDistribuicao.classList.remove('d-none');
            btnFinalizar.disabled = true;
        } else {
            erroDistribuicao.classList.add('d-none');
            btnFinalizar.disabled = false;
        }

        const totalValor = (meias * 10) + (inteiras * 20);
        valorTotal.innerText = totalValor.toFixed(2);

        dadosCarrinho.innerHTML = '';
        let usados = 0;

        for (let i = 0; i < meias && usados < total; i++, usados++) {
            dadosCarrinho.innerHTML += `
                <input type="hidden" name="AssentosSelecionados" value="${assentosSelecionados[usados]}" />
                <input type="hidden" name="TipoEntrada[${assentosSelecionados[usados]}]" value="meia" />
            `;
        }

        for (let i = 0; i < inteiras && usados < total; i++, usados++) {
            dadosCarrinho.innerHTML += `
                <input type="hidden" name="AssentosSelecionados" value="${assentosSelecionados[usados]}" />
                <input type="hidden" name="TipoEntrada[${assentosSelecionados[usados]}]" value="inteira" />
            `;
        }
    }

    checkboxes.forEach(cb => {
        cb.addEventListener('change', function () {
            const assentoId = this.dataset.id;
            const icone = document.querySelector('.icone-assento-' + assentoId);

            if (this.checked) {
                icone.classList.remove('text-success');
                icone.classList.add('text-warning');
            } else {
                icone.classList.remove('text-warning');
                icone.classList.add('text-success');
            }

            atualizarCarrinho();
        });
    });

    meiasInput.addEventListener('input', atualizarCarrinho);
    inteirasInput.addEventListener('input', atualizarCarrinho);

    maisMeia.addEventListener('click', () => {
        let meias = parseInt(meiasInput.value) || 0;
        let inteiras = parseInt(inteirasInput.value) || 0;
        let total = Array.from(checkboxes).filter(cb => cb.checked).length;

        if (meias + inteiras < total) {
            meiasInput.value = meias + 1;
            atualizarCarrinho();
        }
    });

    menosMeia.addEventListener('click', () => {
        let meias = parseInt(meiasInput.value) || 0;
        if (meias > 0) {
            meiasInput.value = meias - 1;
            atualizarCarrinho();
        }
    });

    maisInteira.addEventListener('click', () => {
        let meias = parseInt(meiasInput.value) || 0;
        let inteiras = parseInt(inteirasInput.value) || 0;
        let total = Array.from(checkboxes).filter(cb => cb.checked).length;

        if (meias + inteiras < total) {
            inteirasInput.value = inteiras + 1;
            atualizarCarrinho();
        }
    });

    menosInteira.addEventListener('click', () => {
        let inteiras = parseInt(inteirasInput.value) || 0;
        if (inteiras > 0) {
            inteirasInput.value = inteiras - 1;
            atualizarCarrinho();
        }
    });

    const btnAbrir = document.getElementById('btnAbrirCarrinho');
    const carrinho = new bootstrap.Offcanvas('#carrinhoOffcanvas', { backdrop: false });

    btnAbrir.addEventListener('click', () => {
        carrinho.show();
        setTimeout(() => {
            document.body.classList.remove('offcanvas-open');
            document.body.style.overflow = 'auto';
        }, 200);
        atualizarCarrinho();
    });
});
