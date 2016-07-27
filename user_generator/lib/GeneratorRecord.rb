require "./lib/RU"
require "./lib/BY"
require "./lib/EN"

module GeneratorRecord
	class Factory
		def self.factory(location)
			case location
			when "RU" then RU::Generator	
			when "BY" then BY::Generator
			when "US" then US::Generator
			end		
		end
	end
end