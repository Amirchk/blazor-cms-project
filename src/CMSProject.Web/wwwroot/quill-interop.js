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
                    ['link'],
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
    },

    insertImage: function (quillElement, imageUrl, altText) {
        var quill = Quill.find(quillElement);
        if (quill) {
            var range = quill.getSelection(true);
            var index = range ? range.index : quill.getLength();
            
            quill.insertEmbed(index, 'image', imageUrl);
            quill.setSelection(index + 1);
            
            // Update content after insertion
            var html = quill.root.innerHTML;
            // Trigger change event manually
            quill.root.dispatchEvent(new Event('text-change'));
        }
    }
};