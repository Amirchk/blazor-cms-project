window.quillInterop = {
    createQuill: function (quillElement, dotNetHelper) {
        if (!quillElement) {
            console.error('Quill element not found');
            return null;
        }

        var quill = new Quill(quillElement, {
            theme: 'snow',
            modules: {
                toolbar: [
                    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    ['blockquote', 'code-block'],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    [{ 'script': 'sub' }, { 'script': 'super' }],
                    [{ 'indent': '-1' }, { 'indent': '+1' }],
                    [{ 'direction': 'rtl' }],
                    [{ 'color': [] }, { 'background': [] }],
                    [{ 'align': [] }],
                    ['link', 'image'],
                    ['clean']
                ]
            }
        });

        // Update Blazor when content changes
        quill.on('text-change', function () {
            var html = quill.root.innerHTML;
            dotNetHelper.invokeMethodAsync('UpdateContent', html);
        });

        return quill;
    },

    getContent: function (quillElement) {
        var quill = Quill.find(quillElement);
        return quill ? quill.root.innerHTML : '';
    },

    setContent: function (quillElement, content) {
        var quill = Quill.find(quillElement);
        if (quill && content) {
            quill.root.innerHTML = content;
        }
    }
};