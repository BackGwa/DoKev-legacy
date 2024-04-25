/* FOREACH : 리스트 반복문을 검사하고 변경합니다. */
string FOREACH(string line, int line_number, string before_code) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|으로 해서|로 해서|으로|로|각각 ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    while (regex_search(it, line.cend(), matches, pattern)) {
        const string match = matches[0];
        result += matches.prefix();

        if (match == "으로 해서" || match == "로 해서" || match == "으로" || match == "로")
            result += "<-for-each->";
        else if (match == "각각 ") {
            result += "<-for-each-item->";
        }
        else
            result += matches[0];

        it = matches[0].second;
    }

    result += string(it, line.cend());

    if (Findword(result, "<-particle->") != -1 &&
        Findword(result, "<-for-each->") != -1 &&
        Findword(result, "<-for-each-item->") != -1 ) {

        const string list = Split(result, "<-particle->")[0];
        const string item = Split(result, "<-for-each-item->")[1];
        
        result = "for " + Split(Split(item, "<-for-each->")[0], ">")[1] + " in " + list + ":";
    } else if ((Findword(result, "<-for-each->") != -1 &&
                Findword(result, "<-for-each-item->") != -1)
                && Findword(result, "<-particle->") == -1) {
        SyntaxError(line_number + 1,
                    FOR_EACH_NON_PARTICLE_TITLE,
                    FOR_EACH_NON_PARTICLE_MESSAGE,
                    before_code,
                    " 각",
                    FOR_EACH_NON_PARTICLE_SUGGESTION_CONTENT,
                    FOR_EACH_NON_PARTICLE_INDEX);
    } else if (Findword(result, "<-for-each->") != -1 &&
                Findword(result, "<-for-each-item->") == -1) {
        SyntaxError(line_number + 1,
                    FOR_EACH_NON_ITEM_TITLE,
                    FOR_EACH_NON_ITEM_MESSAGE,
                    before_code,
                    " ",
                    FOR_EACH_NON_ITEM_SUGGESTION_CONTENT,
                    FOR_EACH_NON_ITEM_INDEX);
    } else if (Findword(result, "<-for-each-item->") != -1 &&
                Findword(result, "<-for-each->") == -1) {
        SyntaxError(line_number + 1,
                    FOR_EACH_ONLY_ITEM_TITLE,
                    FOR_EACH_ONLY_ITEM_MESSAGE,
                    before_code,
                    "EOW",
                    FOR_EACH_ONLY_ITEM_SUGGESTION_CONTENT,
                    FOR_EACH_ONLY_ITEM_INDEX);
    }
    
    return result;
}