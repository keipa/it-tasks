#encoding: UTF-8
require 'ffaker'
# @person =FFaker::NameRU.last_name(for_sex=:female) +
#     " "+
#     FFaker::NameRU.first_name(for_sex=:female) +
#     " "+
#     FFaker::NameRU.patronymic(for_sex=:female)
# @address =  FFaker::AddressRU.city.encoding+
#     FFaker::AddressRU.street_name.encoding+
#     " "  +
#     FFaker::AddressRU.street_number.encoding+
#     ", "+
#     FFaker::PhoneNumberAU.international_home_work_phone_number.encoding


puts FFaker::AddressRU.city.encoding
puts FFaker::AddressRU.street_name.encoding
puts FFaker::AddressRU.street_number.encoding
puts FFaker::PhoneNumberAU.international_home_work_phone_number.encoding



# puts @person.encoding
# puts @address
#          .encode "UTF-8"

