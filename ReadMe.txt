���������� ��������� �� ���������� ������ ���������� � ��������� ����� �� ������������� �����.
�������������:
var result = Plural.Format(CultureInfo.GetCulture("ru-RU"), "{0} {0:#�����;�����;����;�����", 1);
// result == "1 �����"

var result = Plural.Format(CultureInfo.GetCulture("en-US"), "{0} {0:#book;books}", 4);
// result == "4 books"

���������� � ������� ���� ����� ��� ����������� ����� ����� �������� �� ��������
http://unicode.org/repos/cldr-tmp/trunk/diff/supplemental/language_plural_rules.html