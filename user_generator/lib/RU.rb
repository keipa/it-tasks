require "./lib/Base"
require "random_data"
require 'yaml'
require 'pry'
require 'ostruct'
require "ryba"
require "./lib/Damage"


module RU
	class Generator < Base::Generator
		def self.generate count,  probability
			users = 0
      fakedata = RU::fake_data
      while users < count
        users += 1
        name = RU::name
        address = RU::address_and_phone fakedata
        puts Damage::execute("#{name}, #{address}", probability)+ "\n"
			end
		end
  end

		def self.name
			Ryba::Name.full_name
		end

		def self.address_and_phone fakedata
			fake  = fakedata.sample
			phone = "+#{fake["phone_code"]}-#{rand(10..99)}-#{rand(10..99)}-#{rand(10000..99999)}"
			address = "#{Ryba::Address.street},д.,#{rand(1..200)}, #{fake["city"]}, #{fake["state"]}, #{fake["zip_code"]}, Россия"
			"#{address}, #{phone}"
		end

		def self.fake_data
			@data ||= open('./fixtures/RU.cfg') { |f| YAML.load(f) }
		end
	end
