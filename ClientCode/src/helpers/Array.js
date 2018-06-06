Array.prototype.groupBy = function (attrFunctor) {
	debugger
	var results = {}
	var ttt = attrFunctor(this[0])
	for (var x in this) {
		var attr = String(attrFunctor(this[x]))
		(results[attr] = results[attr] || []).push(x)
	}
	return results
}
