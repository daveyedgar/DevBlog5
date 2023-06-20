let index = 0;

function AddTag() {
    var newTag = document.getElementById("NewTag");
    var chooseTag = document.getElementById("ChooseTag");
    //var tagEntry = document.getElementById("TagEntry");

    if (newTag.value != "") {
        tagEntry = newTag;
    } else {
        tagEntry = chooseTag;
    }

    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bold'>${searchResult.toUpperCase()}</span>`
        });
    } else {
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;
    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<spanclass='font-weight-bold'>Choose a tag to delete.</span>"
        });
        return true;
    }
    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount
        } else 
            tagCount - 0;
        index--;
        
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

if (tagValues != "") {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}


function search(str) {
    if (str == "") {
        return "Empty tags are not permitted."
    }

    var tagsElement = document.getElementById("TagList");
    if (tagsElement) {
        let options = tagsElement.options;
        for (let i = 0; i < options.length; i++) {
            if (options[i].value == str) {
                return `The tag ${str} already exists.`
            }
        }
    }
}

    const swalWithDarkButton = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
        },
        imageUrl: '/assets/images/oops.png',
        //timer: 5000,
        buttonSyling: false
    });