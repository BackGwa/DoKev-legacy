/* FOREACH : 리스트 반복문을 검사하고 변경합니다. */
string FOREACH(string line, int line_number, string before_code) {
    regex pattern("\"([^\"]*)\"|'([^']*)'|으로 해서|각각 ");
    smatch matches;
    string result;

    auto it = line.cbegin();

    if (Findword(line, "<-particle->") != -1) {
        while (regex_search(it, line.cend(), matches, pattern)) {
            const string match = matches[0];
            result += matches.prefix();

            if (match == "으로 해서")
                result += "<-for-each->";
            else if (match == "각각 ") {
                result += "<-for-each-item->";
            }
            else
                result += matches[0];

            it = matches[0].second;
        }
    }

    result += string(it, line.cend());

    if (Findword(result, "<-particle->") != -1 &&
        Findword(result, "<-for-each->") != -1 &&
        Findword(result, "<-for-each-item->") != -1 ) {

        const string list = Split(result, "<-particle->")[0];
        const string item = Split(result, "<-for-each-item->")[1];
        
        result = "for " + Split(Split(item, "<-for-each->")[0], ">")[1] + " in " + list + ":";
    }
    
    return result;
}