require "./lib/Options"
require "./lib/Generator"
# require 'profile'

options = Options.new(ARGV)

if options.errors?
	options.errors.each { |e| puts e }
	exit(0)
end

Generator.generate options

