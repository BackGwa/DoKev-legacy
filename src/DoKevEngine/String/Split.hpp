#pragma once

/* Split : 문자열을 나눕니다. */
std::vector<std::string> Split(const std::string s, const std::string divid) {
	std::vector<std::string> v;
	int start = 0;
	int d = s.find(divid);
	while (d != -1){
		v.push_back(s.substr(start, d - start));
		start = d + 1;
		d = s.find(divid, start);
	} 
	v.push_back(s.substr(start, d - start));

	return v;
}