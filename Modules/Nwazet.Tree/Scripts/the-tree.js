$(function() {
    var treeParams = window.the_nwazet_tree,
        serviceUrl = treeParams.serviceUrl,
        res = treeParams.res,
        appendWaitNode = function (parent) {
            parent
                .children(".the-tree-leaf-label, .the-tree-root")
                .after(
                    $("<div></div>")
                        .addClass("the-tree-waits")
                );
        },
        populateBranch = function(parent) {
            $.ajax({
                url: serviceUrl
                    .replace("__id__", encodeURIComponent(parent.data("id")))
                    .replace("__type__", encodeURIComponent(parent.data("type"))),
                type: "POST",
                dataType: "json",
                success: function(data) {
                    parent
                        .data("children", data)
                        .hide()
                        .empty()
                        .parent()
                        .find(".the-tree-waits")
                        .remove();
                    $.each(data, function() {
                        var childElement = $("<li></li>")
                            .addClass("the-tree-leaf");
                        if (!this.IsLeaf) {
                            childElement.append(
                                $("<div></div>")
                                    .addClass("the-tree-expando-glyph")
                                    .append(
                                        $("<a></a>", {
                                            href: "#"
                                        })
                                            .click(function(e) {
                                                e.preventDefault();
                                                var expandoGlyph = $(this)
                                                    .parent()
                                                    .toggleClass("open");
                                                if (!expandoGlyph.data("already-open")) {
                                                    expandoGlyph.data("already-open", true);
                                                    appendWaitNode(childElement);
                                                    populateBranch(childBranch);
                                                } else {
                                                    childBranch.slideToggle();
                                                }
                                            })
                                    )
                            );
                        }
                        childElement.append(
                            (this.Url ? $("<a></a>", { href: this.Url }) : $("<span></span>"))
                                .addClass("the-tree-leaf-label the-tree-leaf-" + this.Type + (this.CssClass || ""))
                                .text(this.Title)
                        );
                        var childBranch = $("<ul></ul>")
                                .data({
                                    id: this.Id,
                                    type: this.Type
                                })
                                .addClass("the-tree-branch");
                        childElement.append(childBranch);
                        parent.append(childElement).slideDown("slow");
                    });
                },
                error: function() {
                    parent
                        .empty()
                        .append(
                            $("<li></li>")
                                .text(res.loadChildError)
                        );
                }
            });
        },
        neverOpen = true,
        openTag = $("<div></div>")
            .addClass("the-tree-open-tag")
            .append($("<div></div>")
                .append($("<a href=\"#\"></a>")
                    .text(res.openTagLabel)
                    .on("click", function(e) {
                        e.preventDefault();
                        if (neverOpen) {
                            appendWaitNode(theTreeContainer);
                            treeRoot.toggle("slow");
                            populateBranch(treeRoot);
                            neverOpen = false;
                        }
                        else {
                            treeRoot.toggle("slow");
                        }
                        openTag.toggleClass("open");
                    }))
            ),
        treeRoot = $("<ul></ul>")
            .addClass("the-tree-root")
            .data({
                id: "/",
                type: "root"
            })
            .hide(),
        theTreeContainer = $("<div></div>")
            .addClass("the-tree-container")
            .append(openTag)
            .append(treeRoot);
    $("body").append(theTreeContainer);
});