
$(document).ready(() => {
    const form = $('#row_form');
    const inputs = $('.row-value');
    
    form.submit((e) => {
        e.preventDefault();
        const values = $.map(inputs, x => x.value);
        fetch('https://reqbin.com/echo/post/json', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(values)
        })
    })
})