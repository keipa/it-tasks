puts "Hello World!"
require 'ffaker'

puts FFaker::NameRU.last_name(for_sex= :female) +
         " "+
         FFaker::NameRU.first_name(for_sex= :female) +
         " "+
         FFaker::NameRU.patronymic(for_sex= :female)
ў =  14882280
puts "My psk is #{ў}"


#Господин ровда, я стаўлю вам дзесяць баллау