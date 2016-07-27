require "./lib/Base"
require "random_data"
require 'pry'
require 'ostruct'
require "ryba"
require 'csv'
require "./lib/Damage"


module BY
	PHONE_CODE_BY = %w"29 33 44"

	class Generator < Base::Generator
		def self.generate count, probability
			users = 0
			@fake_csv = BY::fake_data
			while users < count
				users += 1
        name = BY::name
				address = BY::address_and_phone @fake_csv
        puts Damage::execute("#{name}, #{address}", probability)+ "\n"
			end
		end
	end

		def self.name
			Ryba::Name.full_nameBY
		end

		def self.address_and_phone fake_csv
			fake  = fake_csv.sample
      ran = rand(100..999)
			phone = " +375-#{PHONE_CODE_BY.sample}-#{ran}-#{100-ran/10}-#{ran/10}"
			address = "#{Ryba::Address.streetBY},д.,#{ran/10}, #{fake["city"]}, #{fake["state"]}, #{fake["zip_code"]}, Беларусь"
			"#{address}, #{phone}"
		end

		def self.fake_data
				@data = []
				CSV.foreach("./fixtures/BY.csv") do |row|
	  			@data += [{"city" => "г. #{row[0]}", "state" => row[2], "zip_code" => row[1]}]
				end
  		@data
		end

end
