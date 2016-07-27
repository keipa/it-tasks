require "./lib/Base"
require "random_data"
require "./lib/Damage"


module US
	class Generator < Base::Generator
		def self.generate count,  probability
      users = 0
      fakedata = US::fake_data
      while users < count
        users += 1
        name = US::name
        address = US::address_and_phone fakedata
        puts Damage::execute("#{name}, #{address}", probability)+ "\n"
      end
		end
	end


		def self.name
			"#{Random.firstname} #{Random.lastname}"
		end

		def self.address_and_phone fakedata
			fake  = fakedata.sample
      phone = "+1-#{rand(10..99)}-#{rand(10..99)}-#{rand(10000..99999)}"
      address = "#{Random.address_line_1}, #{fake.city}, #{fake.state}, #{fake.zip_code}, United States"
      "#{address}, #{phone}"
		end

		def self.fake_data
			@data ||= File.open( "./fixtures/US.txt" ).map {|r| FakeData.new(r)}
		end

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
			"USA"
		end

		def zip_code
			@line[1]
		end

		def phone_code
			@line[6]
		end
	end
end
