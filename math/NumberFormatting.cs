using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naru.math
{
    public class NumberFormatting
    {
        //
        // Write these conversion values as multipliers from the first
        // unit to the second unit.
        //
        // Distance
        public const double MetresToMilimetres = 1000;
        public const double MetresToCentiMetres = 100;
        public const double MetresToKilometres = 0.001;
        public const double MetresToInches = 39.3701;
        public const double MetrestoFeet = 3.28084;
        public const double MetresToYards = 1.09361;

        public const double MetrestoMiles = 0.000621371;

        // Area
        public const double SqMetreToSqMilimetre = 1000000.0;
        public const double SqMetreToSqCentimetre = 10000;
        public const double SqMetreToSqKilometre = 10 ^ -6;
        public const double SqMetreToHectare = 0.0001;
        public const double SqMetreToSqInch = 1550;
        public const double SqMetreToSqFoot = 10.7639;
        public const double SqMetreToSqYard = 1.19599;
        public const double SqMetreToSqMile = 3.861E-07;

        public const double SqMetreToAcre = 0.000247105;
        // Volume
        public const double CubicMetresInCubicMillimetres = 1000000000;
        public const double CubicMetresInCubicCentimetres = 1000000;
        public const double CubicMetresInCupsUS = 4226.75;
        public const double CubicMetresInLitres = 1000;
        public const double CubicMetresInCubicInch = 61023.7;
        public const double CubicMetresInCubicFeet = 35.3147;
        public const double CubicMetresInAcreFeet = 0.000810713194;
        public const double CubicMetresInUSGallons = 264.172;
        public const double CubicMetresInCubicYards = 1.30795062;
        public const double CubicMetresInCubicKm = 10 ^ -10;
        public const double CubicMetresInCubicMiles = 2.39912759 * (10 ^ -10);

        /// <summary>
        /// Measures of linear units, such as "metres"
        /// </summary>
        /// <remarks></remarks>
        public enum LinearUnits
        {
            mm,
            cm,
            m,
            km,
            inch,
            ft,
            yard,
            mile
        }

        /// <summary>
        /// Measures of areas such as "square metres"
        /// </summary>
        /// <remarks></remarks>
        public enum AreaUnits
        {
            sqmm,
            sqcm,
            sqm,
            sqkm,
            hectare,
            sqin,
            sqft,
            sqyd,
            sqmi,
            acre
        }

        /// <summary>
        /// Measures of volumetric units, such as metres cubed
        /// </summary>
        /// <remarks></remarks>
        public enum VolumeUnits
        {
            mm3,
            cm3,
            cupsUS,
            litres,
            m3,
            inch3,
            feet3,
            gallons,
            yard3,
            acrefeet,
            km3,
            mi3
        }

        /// <summary>
        /// Convert from one linear unit to another
        /// </summary>
        /// <param name="eFrom">The input linear units from which you want to convert</param>
        /// <param name="eTo">The output linear units for the result</param>
        /// <param name="fValue"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double Convert(LinearUnits eFrom, LinearUnits eTo, double fValue)
        {

            double fResult = 0;

            try
            {
                switch (eFrom)
                {

                    case LinearUnits.mm:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = fValue;
                                break;
                            case LinearUnits.cm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetresToMilimetres;
                                break;
                            case LinearUnits.km:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case LinearUnits.cm:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = fValue;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetresToCentiMetres;
                                break;
                            case LinearUnits.km:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case LinearUnits.m:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = fValue * NumberFormatting.MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = fValue * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue;
                                break;
                            case LinearUnits.km:
                                fResult = fValue * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = fValue * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = fValue * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = fValue * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = fValue * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;

                    case LinearUnits.km:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetresToKilometres;
                                break;
                            case LinearUnits.km:
                                fResult = fValue;
                                break;
                            case LinearUnits.inch:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case LinearUnits.inch:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetresToInches;
                                break;
                            case LinearUnits.km:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = fValue;
                                break;
                            case LinearUnits.ft:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case LinearUnits.ft:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetrestoFeet;
                                break;
                            case LinearUnits.km:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = fValue;
                                break;
                            case LinearUnits.yard:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case LinearUnits.yard:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetresToYards;
                                break;
                            case LinearUnits.km:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = fValue;
                                break;
                            case LinearUnits.mile:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoMiles;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case LinearUnits.mile:
                        switch (eTo)
                        {
                            case LinearUnits.mm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToMilimetres;
                                break;
                            case LinearUnits.cm:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToCentiMetres;
                                break;
                            case LinearUnits.m:
                                fResult = fValue / MetrestoMiles;
                                break;
                            case LinearUnits.km:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToKilometres;
                                break;
                            case LinearUnits.inch:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToInches;
                                break;
                            case LinearUnits.ft:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetrestoFeet;
                                break;
                            case LinearUnits.yard:
                                fResult = Convert(eFrom, LinearUnits.m, fValue) * MetresToYards;
                                break;
                            case LinearUnits.mile:
                                fResult = fValue;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    default:

                        throw new Exception();
                }

            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception("Unhandled volume unit conversation", ex);
                ex2.Data.Add("From units", eFrom);
                ex2.Data.Add("To units", eTo);
                throw ex2;
            }

            return fResult;

        }

        public static double Convert(AreaUnits eFrom, AreaUnits eTo, double fValue)
        {

            double fResult = 0;


            try
            {
                switch (eFrom)
                {

                    case AreaUnits.sqmm:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqcm:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqm:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = fValue * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = fValue * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqkm:
                                fResult = fValue * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = fValue * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = fValue * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = fValue * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = fValue * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = fValue * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = fValue * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqkm:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqkm:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqin:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqft:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqyd:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = fValue;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.sqmi:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = fValue;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.acre:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = fValue;
                                break;
                            case AreaUnits.hectare:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToHectare;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case AreaUnits.hectare:
                        switch (eTo)
                        {
                            case AreaUnits.sqmm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMilimetre;
                                break;
                            case AreaUnits.sqcm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqCentimetre;
                                break;
                            case AreaUnits.sqm:
                                fResult = fValue / NumberFormatting.SqMetreToHectare;
                                break;
                            case AreaUnits.sqkm:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqKilometre;
                                break;
                            case AreaUnits.sqin:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqInch;
                                break;
                            case AreaUnits.sqft:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqFoot;
                                break;
                            case AreaUnits.sqyd:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqYard;
                                break;
                            case AreaUnits.sqmi:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToSqMile;
                                break;
                            case AreaUnits.acre:
                                fResult = Convert(eFrom, AreaUnits.sqm, fValue) * NumberFormatting.SqMetreToAcre;
                                break;
                            case AreaUnits.hectare:
                                fResult = fValue;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    default:

                        throw new Exception();
                }


            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception("Unhandled volume unit conversation", ex);
                ex2.Data.Add("From units", eFrom);
                ex2.Data.Add("To units", eTo);
                throw ex2;
            }

            return fResult;

        }

        public static double Convert(VolumeUnits eFrom, VolumeUnits eTo, double fValue)
        {

            double fResult = 0;
            try
            {
                switch (eFrom)
                {

                    case VolumeUnits.mm3:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.cm3:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.cupsUS:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = fValue;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.litres:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = fValue;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.m3:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = fValue * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = fValue * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = fValue * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = fValue * CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = fValue * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = fValue * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = fValue * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = fValue * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = fValue * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.km3:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicKm;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = fValue;
                                break;
                        }

                        break;
                    case VolumeUnits.acrefeet:

                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = fValue;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.inch3:

                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.feet3:

                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.gallons:

                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = fValue;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:

                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.yard3:

                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.mi3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                            default:
                                throw new Exception();
                        }

                        break;
                    case VolumeUnits.mi3:
                        switch (eTo)
                        {
                            case VolumeUnits.mm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicMillimetres;
                                break;
                            case VolumeUnits.cm3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicCentimetres;
                                break;
                            case VolumeUnits.cupsUS:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCupsUS;
                                break;
                            case VolumeUnits.litres:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInLitres;
                                break;
                            case VolumeUnits.m3:
                                fResult = fValue / NumberFormatting.CubicMetresInCubicMiles;
                                break;
                            case VolumeUnits.acrefeet:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInAcreFeet;
                                break;
                            case VolumeUnits.inch3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicInch;
                                break;
                            case VolumeUnits.feet3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicFeet;
                                break;
                            case VolumeUnits.gallons:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInUSGallons;
                                break;
                            case VolumeUnits.yard3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicYards;
                                break;
                            case VolumeUnits.mi3:
                                fResult = fValue;
                                break;
                            case VolumeUnits.km3:
                                fResult = Convert(eFrom, VolumeUnits.m3, fValue) * NumberFormatting.CubicMetresInCubicKm;
                                break;
                        }

                        break;
                    default:

                        throw new Exception();
                }

            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception("Unhandled volume unit conversation", ex);
                ex2.Data["From units"] = eFrom;
                ex2.Data["To units"] = eTo;
                throw ex2;
            }

            return fResult;

        }

        public static string GetUnitsAsString(LinearUnits eUnits, bool bParentheses = false, int nPower = 1)
        {

            string sResult = string.Empty;
            switch (eUnits)
            {
                case LinearUnits.mm:
                    sResult = "mm";
                    break;
                case LinearUnits.cm:
                    sResult = "cm";
                    break;
                case LinearUnits.m:
                    sResult = "m";
                    break;
                case LinearUnits.km:
                    sResult = "km";
                    break;
                case LinearUnits.inch:
                    sResult = "in";
                    break;
                case LinearUnits.ft:
                    sResult = "ft";
                    break;
                case LinearUnits.yard:
                    sResult = "yd";
                    break;
                case LinearUnits.mile:
                    sResult = "mile";
                    break;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eUnits.ToString();
                    throw ex;
            }

            string unitLabel = null;
            switch (nPower)
            {

                case 2:
                    unitLabel = "²";
                    break;
                case 3:
                    unitLabel = "³";

                    break;
            }

            if (bParentheses)
            {
                sResult = " (" + sResult + unitLabel + ")";
            }

            return sResult;

        }

        public static string GetUnitsAsString(AreaUnits eUnits, bool bParentheses = false)
        {

            string sResult = string.Empty;
            switch (eUnits)
            {
                case AreaUnits.sqmm:
                    sResult = "mm²";
                    break;
                case AreaUnits.sqcm:
                    sResult = "cm²";
                    break;
                case AreaUnits.sqm:
                    sResult = "m²";
                    break;
                case AreaUnits.sqkm:
                    sResult = "km²";
                    break;
                case AreaUnits.sqin:
                    sResult = "in²";
                    break;
                case AreaUnits.sqft:
                    sResult = "ft²";
                    break;
                case AreaUnits.sqyd:
                    sResult = "yd²";
                    break;
                case AreaUnits.sqmi:
                    sResult = "mi²";
                    break;
                case AreaUnits.acre:
                    sResult = "acres";
                    break;
                case AreaUnits.hectare:
                    sResult = "hectares";
                    break;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eUnits.ToString();
                    throw ex;
            }

            if (bParentheses)
            {
                sResult = " (" + sResult + ")";
            }
            return sResult;

        }

        public static string GetUnitsAsString(VolumeUnits eUnits, bool bParentheses = false)
        {

            string sResult = string.Empty;
            switch (eUnits)
            {
                case VolumeUnits.mm3:
                    sResult = "mm³";
                    break;
                case VolumeUnits.cm3:
                    sResult = "cm³";
                    break;
                case VolumeUnits.cupsUS:
                    sResult = "US cups";
                    break;
                case VolumeUnits.litres:
                    sResult = "l";
                    break;
                case VolumeUnits.m3:
                    sResult = "m³";
                    break;
                case VolumeUnits.inch3:
                    sResult = "in³";
                    break;
                case VolumeUnits.feet3:
                    sResult = "ft³";
                    break;
                case VolumeUnits.acrefeet:
                    sResult = "acre ft";
                    break;
                case VolumeUnits.yard3:
                    sResult = "yd³";
                    break;
                case VolumeUnits.gallons:
                    sResult = "gallons";
                    break;
                case VolumeUnits.mi3:
                    sResult = "mi³";
                    break;
                case (VolumeUnits.km3):
                    sResult = "km³";
                    break;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eUnits.ToString();
                    throw ex;
            }

            if (bParentheses)
            {
                sResult = " (" + sResult + ")";
            }
            return sResult;

        }

        //public static LinearUnits GetLinearUnits(ILinearUnit eLinearUnits)
        //{

        //    switch (eLinearUnits.Name.ToLower)
        //    {
        //        case "meter":
        //        case "metre":

        //            return LinearUnits.m;
        //        case "kilometer":
        //        case "kilometre":

        //            return LinearUnits.km;
        //        case "feet":
        //        case "us_feet":
        //        case "ft":

        //            return LinearUnits.ft;
        //        default:
        //            Exception ex = new Exception("Unhandled linear units");
        //            ex.Data("Linear units") = eLinearUnits.Name;
        //            throw ex;
        //    }

        //}

        public static AreaUnits GetAreaUnitsRaw(LinearUnits eLinearUnit)
        {

            switch (eLinearUnit)
            {

                case LinearUnits.mm:

                    return AreaUnits.sqmm;
                case LinearUnits.cm:

                    return AreaUnits.sqcm;
                case LinearUnits.m:

                    return AreaUnits.sqm;
                case LinearUnits.km:

                    return AreaUnits.sqkm;
                case LinearUnits.inch:

                    return AreaUnits.sqin;
                case LinearUnits.ft:

                    return AreaUnits.sqft;
                case LinearUnits.yard:

                    return AreaUnits.sqyd;
                case LinearUnits.mile:

                    return AreaUnits.sqmi;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();
                    throw ex;
            }

        }

        public static AreaUnits GetAreaUnitsScaled(LinearUnits eLinearUnit)
        {

            switch (eLinearUnit)
            {
                case LinearUnits.mm:
                case LinearUnits.cm:
                case LinearUnits.m:
                case LinearUnits.km:

                    return AreaUnits.hectare;
                case LinearUnits.inch:
                case LinearUnits.ft:
                case LinearUnits.yard:
                case LinearUnits.mile:
                    return AreaUnits.acre;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();
                    throw ex;
            }

        }

        public static VolumeUnits GetVolumeUnitsRaw(LinearUnits eLinearUnit)
        {

            switch (eLinearUnit)
            {
                case LinearUnits.mm:

                    return VolumeUnits.mm3;
                case LinearUnits.cm:

                    return VolumeUnits.cm3;
                case LinearUnits.m:

                    return VolumeUnits.m3;
                case LinearUnits.km:

                    return VolumeUnits.km3;
                case LinearUnits.inch:

                    return VolumeUnits.inch3;
                case LinearUnits.ft:

                    return VolumeUnits.feet3;
                case LinearUnits.yard:

                    return VolumeUnits.yard3;
                case LinearUnits.mile:

                    return VolumeUnits.mi3;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();
                    throw ex;
            }

        }

        public static VolumeUnits GetVolumeUnitsScaled(LinearUnits eLinearUnit)
        {

            switch (eLinearUnit)
            {
                case LinearUnits.mm:
                case LinearUnits.cm:
                case LinearUnits.m:
                case LinearUnits.km:

                    return VolumeUnits.m3;
                case LinearUnits.inch:
                case LinearUnits.ft:
                case LinearUnits.yard:
                case LinearUnits.mile:
                    return VolumeUnits.acrefeet;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();
                    throw ex;
            }

        }

        public static bool IsMetric(LinearUnits eLinearUnit)
        {
            switch (eLinearUnit)
            {
                case LinearUnits.mm:
                case LinearUnits.cm:
                case LinearUnits.m:
                case LinearUnits.km:
                    return true;
                case LinearUnits.inch:
                case LinearUnits.ft:
                case LinearUnits.yard:
                case LinearUnits.mile:
                    return false;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();

                    throw ex;
            }
        }

        public static bool IsMetric(AreaUnits eLinearUnit)
        {
            switch (eLinearUnit)
            {
                case AreaUnits.sqm:
                case AreaUnits.sqkm:
                case AreaUnits.hectare:
                    return true;
                case AreaUnits.sqft:
                case AreaUnits.acre:
                    return false;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();

                    throw ex;
            }
        }

        public static bool IsMetric(VolumeUnits eLinearUnit)
        {
            switch (eLinearUnit)
            {
                case VolumeUnits.m3:
                case VolumeUnits.mm3:
                case VolumeUnits.cm3:
                case VolumeUnits.litres:
                    return true;
                case VolumeUnits.feet3:
                case VolumeUnits.acrefeet:
                    return false;
                default:
                    Exception ex = new Exception("Unhandled units");
                    ex.Data["Units"] = eLinearUnit.ToString();

                    throw ex;
            }
        }

        //public static LinearUnits GetLinearUnitsFromESRI(ESRI.ArcGIS.Geometry.ILinearUnit eESRILinearUnits)
        //{

        //    string sUnits = GISDataStructures.GetLinearUnitsAsString(eESRILinearUnits);
        //    return GetLinearUnitsFromString(sUnits);

        //}

        public static LinearUnits GetLinearUnitsFromString(string sLinearUnits)
        {

            NumberFormatting.LinearUnits eResult = LinearUnits.m;
            switch (sLinearUnits.ToLower())
            {
                case "mm":
                    eResult = LinearUnits.mm;
                    break;
                case "cm":
                    eResult = LinearUnits.cm;
                    break;
                case "m":
                    eResult = LinearUnits.m;
                    break;
                case "km":
                    eResult = LinearUnits.km;
                    break;
                case "in":
                    eResult = LinearUnits.inch;
                    break;
                case "ft":
                    eResult = LinearUnits.ft;
                    break;
                case "yd":
                    eResult = LinearUnits.yard;
                    break;
                case "mi":
                    eResult = LinearUnits.mile;
                    break;
                default:
                    Exception ex = new Exception("Unhandled linear unit type.");
                    ex.Data["Unit"] = sLinearUnits;
                    throw ex;
            }
            return eResult;
        }


        public static void TestBench()
        {
            System.Diagnostics.Debug.Print("Converting 34.456 metres to kilometres: " + Convert(LinearUnits.m, LinearUnits.km, 34.456) + ", expecting 0.034456", 34.456);
            System.Diagnostics.Debug.Print("Converting 10000423 cubic feet to acre feet: " + Convert(VolumeUnits.feet3, VolumeUnits.acrefeet, 10000423) + ", expecting 229.58", 10000423);

            double[] dblArray = new double[] {
                11.3,
                0.005,
                10000,
                17.5,
                4.64
            };

            Array areaUnitsSet = default(Array);
            areaUnitsSet = System.Enum.GetValues(typeof(NumberFormatting.AreaUnits));
            NumberFormatting.AreaUnits eLinear = default(NumberFormatting.AreaUnits);

          System.Diagnostics.Debug.Print("AREA CONVERSION TESTS {0}{0}", Environment.NewLine);
            double dblResult = 0;
            foreach (NumberFormatting.AreaUnits eLinear_loopVariable in areaUnitsSet)
            {
                eLinear = eLinear_loopVariable;
                for (int i = 0; i <= dblArray.Count() - 1; i++)
                {
                    dblResult = Convert(eLinear, AreaUnits.acre, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.acre, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.hectare, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.hectare, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqcm, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqcm, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqft, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqft, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqin, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqin, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqkm, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqkm, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqm, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqm, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqmi, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqmi, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqmm, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqmm, dblResult.ToString()));

                    dblResult = Convert(eLinear, AreaUnits.sqyd, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to {2} equals {3}", dblArray[i].ToString(), eLinear.ToString(), AreaUnits.sqyd, dblResult.ToString()));
                }

            }

            Array volumeUnitsSet = default(Array);
            volumeUnitsSet = System.Enum.GetValues(typeof(NumberFormatting.VolumeUnits));
            NumberFormatting.VolumeUnits eVolume = default(NumberFormatting.VolumeUnits);

           System.Diagnostics.Debug.Print("{0}{0}VOLUME CONVERSION TESTS {0}{0}", Environment.NewLine);

            foreach (NumberFormatting.VolumeUnits eVolume_loopVariable in volumeUnitsSet)
            {
                eVolume = eVolume_loopVariable;
                for (int i = 0; i <= dblArray.Count() - 1; i++)
                {
                    dblResult = Convert(eVolume, VolumeUnits.acrefeet, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.acrefeet, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.cm3, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.cm3, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.cupsUS, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.cupsUS, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.feet3, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.feet3, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.gallons, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.gallons, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.inch3, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.inch3, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.litres, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in  {1} to  {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.litres, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.m3, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.m3, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.mm3, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.mm3, dblResult.ToString()));

                    dblResult = Convert(eVolume, VolumeUnits.yard3, dblArray[i]);
                    System.Diagnostics.Debug.Print(string.Format("{0} in {1} to {2} equals {3}", dblArray[i].ToString(), eVolume.ToString(), VolumeUnits.yard3, dblResult.ToString()));
                }

            }

            //Debug.Assert(Math.Round(Convert(AreaUnits.sqm, AreaUnits.sqft, 11.3), 3) = 121.632)
            //Debug.Assert(Math.Round(Convert(AreaUnits.acre, AreaUnits.sqft, 11.3), 0) = 492228)
            //Debug.Assert(Math.Round(Convert(AreaUnits.sqin, AreaUnits.sqmm, 11.3), 3) = 7290.308)
        }

    }

    /// <summary>
    /// Use this class to display the linear units in a dropdownlist
    /// </summary>
    /// <remarks></remarks>
    public class LinearUnitClass
    {
        private NumberFormatting.LinearUnits m_eLinearUnit;

        private string m_sDisplayName;
        public LinearUnitClass(string sDisplayName, NumberFormatting.LinearUnits eLinearUnit)
        {
            m_sDisplayName = sDisplayName;
            m_eLinearUnit = eLinearUnit;
        }

        public LinearUnitClass(NumberFormatting.LinearUnits eLinearUnit)
        {
            m_sDisplayName = NumberFormatting.GetUnitsAsString(eLinearUnit);
            m_eLinearUnit = eLinearUnit;
        }

        public NumberFormatting.LinearUnits LinearUnit
        {
            get { return m_eLinearUnit; }
        }

        public override string ToString()
        {
            return m_sDisplayName;
        }

        public object GetUnitsAsString(bool bParentheses = false)
        {
            return NumberFormatting.GetUnitsAsString(m_eLinearUnit, bParentheses);
        }
    }
}