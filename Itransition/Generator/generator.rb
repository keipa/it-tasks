#!/usr/bin/env ruby
# encoding: utf-8

require "rainbow/ext/string"
require "parallel"
require_relative "ffaker_extended.rb"

REGIONS = { :US => { :name => FFaker::Name,   :address => FFaker::Address   },
            :RU => { :name => FFaker::NameRU, :address => FFaker::AddressRU },
            :BY => { :name => FFaker::NameBY, :address => FFaker::AddressBY } }
ALPHABET = ("a".."z").to_a.freeze
PROCESS_COUNT = 7

error_functions = [

    lambda do |human|
      i = rand(human.length)
      human[i] = ''
    end,

    lambda do |human|
      human.insert rand(human.length), ALPHABET.sample
    end,

    lambda do |human|
      length = human.length
      i = rand(length - 1)
      human[i], human[i + 1] = human[i + 1], human[i]
    end

]

def input_error
  $stderr.puts <<-INFO
  Input error
  Usage:
  #{ "[" + "ruby".bright + "] " +
      __FILE__.bright +
      "{ #{"US".bright} | #{"RU".bright} |#{"BY".bright} } " +
      "people count".underline + " " + "error count".underline
  }
  INFO
  exit
end

def args_wrong (people_count, region, err_count)
  people_count < 1 || region == nil && err_count == nil
end

input_error if ARGV.length != 3

region = REGIONS[ARGV[0].intern]
people_count = ARGV[1].to_i
err_count = ARGV[2].to_i

input_error if args_wrong people_count, region, err_count
region[:address].load_db

people_per_proc = [people_count / PROCESS_COUNT] * PROCESS_COUNT
people_per_proc[0] += people_count % PROCESS_COUNT

error_per_human = err_count / people_count

Parallel.each(people_per_proc, :in_processes => PROCESS_COUNT) do |count|
  count.times do

    human = ""

    human << region[:name].name

    human << ", " << region[:address].street_address

    human << ", " << region[:address].city_zip_code

    human << ", " << region[:address].phone

    if error_per_human > human.length
      error_per_human = human.length
    end

    if err_count < people_count && err_count > 0
      error_functions.sample.call human
      err_count -= 1
    else
      error_per_human.times do
        error_functions.sample.call human
      end
    end

    printf("#{human}\n")

  end
end

