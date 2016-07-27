require 'yaml'

class FakeData
	def initialize(line)

	@line = line.split(/\t/)
	end

	def city
	@line[2]
	end

	def state
	@line[3]
	end

	def country
	"RU"
	end

	def zip_code
	@line[1]
	end

	def phone_code
	"906"
	end
end




h = []
@data ||= File.open( "./fixtures/RU.txt" ).map { |r| FakeData.new(r) }

	#binding.pry

i = 0
#p @data.size
43538.times{
	h += [{
		"city"=>@data[i].city,
		"state"=>@data[i].state,
		"country"=>@data[i].country,
		"zip_code"=>@data[i].zip_code,
		"phone_code"=>@data[i].phone_code
	}]
	i+=1
}

open('RU.cfg', 'w') { |f| YAML.dump(h, f) }

#h = open('RU.cfg') { |f| YAML.load(f) }
#RU - YAML
#BY - CSV

