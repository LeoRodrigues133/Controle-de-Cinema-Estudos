document.addEventListener("DOMContentLoaded", function () {
    const get = id => document.getElementById(id);
    const checkboxes = document.querySelectorAll('.assento-checkbox');

    const maisMeia = get('maisMeia');
    const menosMeia = get('menosMeia');
    const maisInteira = get('maisInteira');
    const menosInteira = get('menosInteira');

    const meiasInput = get('meiasInput');
    const inteirasInput = get('inteirasInput');

    const erroDistribuicao = get('erroDistribuicao');
    const valorTotal = get('valorTotal');
    const dadosCarrinho = get('dadosCarrinho');
    const btnFinalizar = get('btnFinalizar');
    const restantes = get('restantes');
    const btnAbrir = get('btnAbrirCarrinho');
    const carrinho = new bootstrap.Offcanvas('#carrinhoOffcanvas', { backdrop: false });

    function atualizarVisualAssento(cb, tipo) {
        const id = cb.dataset.id;
        const icone = document.querySelector(`.icone-assento-${id}`);
        const card = document.querySelector(`.card-assento-${id}`);

        if (cb.checked) {
            card.classList.add('border-warning');
        }

        if (cb.checked) {
            card.classList.replace('border-success', 'border-warning');
            icone.classList.remove('bi-emoji-neutral');
            icone.classList.add('bi-emoji-laughing');

            icone.classList.remove('text-white', 'text-info', 'text-warning');

            if (tipo === 'meia') icone.classList.add('text-info');
            else if (tipo === 'inteira') icone.classList.add('text-warning');
            else if (tipo === "indefinido") {
                card.classList.replace('border-success', 'border-warning');
                icone.classList.remove('text-warning', 'text-info', 'text-danger');
                icone.classList.add('text-white');
                icone.classList.remove('bi-emoji-heart-eyes');
                icone.classList.add('bi-emoji-neutral');
            } else {
                // tipo === null → resetar para neutro
                card.classList.replace('border-warning', 'border-success');
                icone.classList.remove('bi-emoji-laughing', 'bi-emoji-heart-eyes');
                icone.classList.add('bi-emoji-neutral', 'text-white');
                icone.classList.remove('text-warning', 'text-info', 'text-danger');
            }
        } else if (!cb.disabled) {
            card.classList.replace('border-warning', 'border-success');
            icone.classList.remove('bi-emoji-laughing');
            icone.classList.add('bi-emoji-neutral', 'text-white');
            icone.classList.remove('text-warning', 'text-info');
        }
    }

    function atualizarCarrinho() {
        let meias = parseInt(meiasInput.value) || 0;
        let inteiras = parseInt(inteirasInput.value) || 0;

        const selecionados = Array.from(checkboxes).filter(cb => cb.checked);
        const total = selecionados.length;

        // Corrige valores
        meias = Math.min(meias, total - inteiras);
        inteiras = Math.min(inteiras, total - meias);

        meiasInput.max = total - inteiras;
        inteirasInput.max = total - meias;

        meiasInput.value = meias;
        inteirasInput.value = inteiras;

        const soma = meias + inteiras;
        restantes.innerText = total - soma;

        const erro = soma > total;
        erroDistribuicao.classList.toggle('d-none', !erro);
        btnFinalizar.disabled = erro;

        valorTotal.innerText = (meias * 17.50 + inteiras * 34.99).toFixed(2);

        // Atualiza carrinho e visual
        dadosCarrinho.innerHTML = '';
        const meiasSelecionados = selecionados.slice(0, meias);
        const inteirasSelecionados = selecionados.slice(meias, meias + inteiras);

        meiasSelecionados.forEach(cb => {
            atualizarVisualAssento(cb, 'meia');
            dadosCarrinho.innerHTML += `
                <input type="hidden" name="AssentosSelecionados" value="${cb.value}" />
                <input type="hidden" name="TipoEntrada[${cb.value}]" value="meia" />
            `;
        });

        inteirasSelecionados.forEach(cb => {
            atualizarVisualAssento(cb, 'inteira');
            dadosCarrinho.innerHTML += `
                <input type="hidden" name="AssentosSelecionados" value="${cb.value}" />
                <input type="hidden" name="TipoEntrada[${cb.value}]" value="inteira" />
            `;
        });

        // Resetar visual dos restantes
        selecionados.forEach(cb => {
            if (!meiasSelecionados.includes(cb) && !inteirasSelecionados.includes(cb)) {
                atualizarVisualAssento(cb, "indefinido");
            }
        });
    }

    // Eventos checkbox
    checkboxes.forEach(cb => {
        cb.addEventListener('change', () => {
            atualizarVisualAssento(cb, null);
            atualizarCarrinho();
        });
    });

    // Eventos de input manual
    meiasInput.addEventListener('input', atualizarCarrinho);
    inteirasInput.addEventListener('input', atualizarCarrinho);

    // Eventos de incremento/decremento
    function alterarValor(input, delta) {
        let valor = parseInt(input.value) || 0;
        const total = Array.from(checkboxes).filter(cb => cb.checked).length;
        const outroValor = input === meiasInput ? parseInt(inteirasInput.value) || 0 : parseInt(meiasInput.value) || 0;

        if (delta > 0 && valor + outroValor < total) input.value = valor + 1;
        if (delta < 0 && valor > 0) input.value = valor - 1;

        atualizarCarrinho();
    }

    maisMeia.addEventListener('click', () => alterarValor(meiasInput, 1));
    menosMeia.addEventListener('click', () => alterarValor(meiasInput, -1));
    maisInteira.addEventListener('click', () => alterarValor(inteirasInput, 1));
    menosInteira.addEventListener('click', () => alterarValor(inteirasInput, -1));

    // Botão abrir carrinho
    btnAbrir.addEventListener('click', () => {
        carrinho.show();
        setTimeout(() => {
            document.body.classList.remove('offcanvas-open');
            document.body.style.overflow = 'auto';
        }, 200);
        atualizarCarrinho();
    });
});
