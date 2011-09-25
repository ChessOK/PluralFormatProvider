Библиотека позволяет не испытывать больше трудностей с переводом слова во множественное число.
Использование:
var result = Plural.Format(CultureInfo.GetCulture("ru-RU"), "{0} {0:#книга;книги;книг;книга", 1);
// result == "1 книга"

var result = Plural.Format(CultureInfo.GetCulture("en-US"), "{0} {0:#book;books}", 4);
// result == "4 books"

Количество и порядок форм слова для конкретного языка нужно уточнять на странице
http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/language_plural_rules.html