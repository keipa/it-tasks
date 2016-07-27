require 'optparse'
require 'parallel'
require "./lib/Damage"
require "./lib/GeneratorRecord"

module Generator
  def self.generate options
	  factory = GeneratorRecord::Factory.factory options.location

    if options.count>100
      multiprocessing(factory, options)

    else factory.generate options.count, options.probability
      end
  end

  def self.multiprocessing(factory, options)
    kernel_work = options.count/4
    Parallel.map(Array.new(4, kernel_work)) do |one_letter|
      factory.generate one_letter, options.probability
    end
  end
end
