class Options
	attr_reader :location, :count, :probability

	#!!!!!!!
	RU = "RU"
	BY = "BY"
	US = "US"

	def errors?
		@errors.any?
	end

	def errors
		@errors
	end

	private

	def initialize(args)
		@errors = []
		@location = RU
		@count = 1
		@probability = 0
		OptionParser.new do |opts|
	  	opts.banner = "Usage: user_generator.rb [options]"

	 		opts.on("-l", "--location [:#{RU}, :#{BY}, :#{US}]", "Location of generated users") do |loc|
				if !loc.match("^#{BY}$|^#{RU}$|^#{US}$").nil?
		    	@location = loc
				else
					@errors << "Wrong location of watermark. Aborted!"
				end
	  	end

	  	opts.on("-c", "--count NUMBER", "Count of records") do |c|
	  		if !c.match("^[0-9]+$").nil?
	    		@count = c.to_i
	  		else
	  			@errors << "Is not number. Aborted! #{c}"
			end
	  	end

	  	opts.on("-p", "--probability NUMBER", "Probability error in record") do |p|
	  		if !p.match("^[0-9]+$").nil?
          @probability = p.to_i
	    	else
          @errors << "Is not number. Aborted! #{c}"
			end
	  	end
		end.parse!
	end
end
